using Microsoft.AspNetCore.Mvc;

using TomasProj.Interfaces;
using TomasProj.Models;

namespace TomasProj.Controllers
{
    [Produces("text/html", "application/json", "application/xml", "application/x-msgpack")]
    public class DocumentsController : Controller
    {
        private readonly IDocumentStorage _documentService;

        public DocumentsController(IDocumentStorage documentService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        public IActionResult GetById(string id)
        {
            var document = _documentService.GetById(id);
            return document == null
                ? NotFound()
                : Ok(document);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var documents = _documentService.GetAll();
            return documents == null
                ? NotFound()
                : Ok(documents);
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

            return Ok(document);
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
                ? Ok(updatedDocument)
                : NotFound();
        }
    }
}
