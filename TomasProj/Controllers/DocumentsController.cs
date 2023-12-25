using System;
using System.Net.Mime;
using System.Text.Json;
using System.Xml.Linq;

using Microsoft.AspNetCore.Mvc;

using TomasProj.Interfaces;
using TomasProj.Models;
using TomasProj.Services;

namespace TomasProj.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly IDocumentStorage _documentService;
        private readonly IFormatResolver _formatResolver;

        public DocumentsController(IDocumentStorage documentService, IFormatResolver formatResolver)
        {
            _documentService = documentService;
            this._formatResolver = formatResolver;
        }

        [HttpGet]
        public IActionResult GetById(string id)
        {
            var document = _documentService.GetById(id);
            return document == null
                ? NotFound()
                : GetFormatedResponse(document);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var documents = _documentService.GetAll();
            return documents == null
                ? NotFound()
                : GetFormatedResponse(documents);
        }

        [HttpPost]
        public IActionResult Index([FromBody] Document document)
        {
            return AddOrUpdate(document);
        }

        [HttpPost]
        public IActionResult AddOrUpdate([FromBody] Document document)
        {
            if (document == null)
            {
                return BadRequest();
            }

            _documentService.AddOrUpdate(document);

            return GetFormatedResponse(document);
        }

        [HttpPut]
        public IActionResult Update(string id, [FromBody] Document updatedDocument)
        {
            if (updatedDocument == null)
            {
                return BadRequest();
            }

            updatedDocument.Id = id;
            return _documentService.Update(updatedDocument)
                ? GetFormatedResponse(updatedDocument)
                : NotFound();
        }

        private IActionResult GetFormatedResponse(object obj)
        {
            string acceptHeader = _formatResolver.GetPrefferedOutputFormat(Request.Headers["Accept"].FirstOrDefault());
            switch (acceptHeader)
            {
                case MediaTypeNames.Application.Json:
                    return Content(JsonSerializer.Serialize(obj), MediaTypeNames.Application.Json, 200);
                case MediaTypeNames.Application.Xml:
                    XElement xmlString = obj switch
                    {
                        Documents documents
                            => documents.GetDocumentsAsXElement(),
                        Document document
                            => document.GetXML(),
                        _ => throw new ArgumentOutOfRangeException(),
                    };
                    return Content(xmlString.ToString(), MediaTypeNames.Application.Xml, 200);
                default:
                    return StatusCode(406, "Not Acceptable");
            }
        }

        private IActionResult Content(string content, string type, int statusCode)
        {
            var ret = Content(content, type);
            ret.StatusCode = statusCode;
            return ret;
        }
    }
}
