using System.Net.Mime;
using System.Text.Json;
using System.Xml.Linq;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Newtonsoft.Json;

using TomasProj.Controllers;
using TomasProj.Interfaces;
using TomasProj.Models;
using TomasProj.Services;

namespace TomasProjXUnit
{
    public class DocumentsControllerTest
    {
        [Fact]
        public void GetAll_CheckOutputTypeXML()
        {
            // Arrange
            var documentServiceMock = new Mock<IDocumentStorage>();
            var expectedDocuments = DocumetStorageDataInit.Documents;

            documentServiceMock.Setup(mock => mock.GetAll()).Returns(expectedDocuments);
            DocumentsController controller = GetController(documentServiceMock, MediaTypeNames.Application.Xml);

            // Act
            var result = controller.GetAll() as ContentResult;

            // Assert
            try
            {
                XDocument.Parse(result.Content);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Not valid XML format: {ex.Message}");
            }
        }

        [Fact]
        public void GetAll_CheckOutputTypeJSON()
        {
            // Arrange
            var documentServiceMock = new Mock<IDocumentStorage>();
            var expectedDocuments = DocumetStorageDataInit.Documents;

            documentServiceMock.Setup(mock => mock.GetAll()).Returns(expectedDocuments);
            DocumentsController controller = GetController(documentServiceMock, MediaTypeNames.Application.Json);

            // Act
            var result = controller.GetAll() as ContentResult;
            var retObj = JsonDocument.Parse(result.Content);

            // Assert
            try
            {
                Assert.Equal(200, result.StatusCode);
                JsonDocument.Parse(result.Content);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Not valid JSON format: {ex.Message}");
            }
        }

        [Fact]
        public void GetAll_ReturnsOk()
        {
            // Arrange
            var documentServiceMock = new Mock<IDocumentStorage>();
            var expectedDocuments = DocumetStorageDataInit.Documents;

            documentServiceMock.Setup(mock => mock.GetAll()).Returns(expectedDocuments);
            DocumentsController controller = GetController(documentServiceMock);

            // Act
            var result = controller.GetAll() as ContentResult;
            var retObj = JsonConvert.DeserializeObject<Documents>(result.Content);

            // Assert
            Assert.NotNull(retObj);
            Assert.Equal(200, result.StatusCode);

            var actualDocuments = Assert.IsAssignableFrom<IEnumerable<Document>>(retObj);
            Assert.Equal(expectedDocuments, actualDocuments);
        }

        [Fact]
        public void GetById_ReturnsOk()
        {
            // Arrange
            string testId = "1";
            var documentServiceMock = new Mock<IDocumentStorage>();
            var expectedDocuments = DocumetStorageDataInit.Documents;

            documentServiceMock.Setup(mock => mock.GetById(testId)).Returns(expectedDocuments.FirstOrDefault());
            DocumentsController controller = GetController(documentServiceMock);

            // Act
            var result = controller.GetById(testId) as ContentResult;
            var retObj = JsonConvert.DeserializeObject<Document>(result.Content);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var actualDocument = Assert.IsAssignableFrom<Document>(retObj);
            Assert.Equal(actualDocument.Id, testId);
        }

        private static DocumentsController GetController(Mock<IDocumentStorage> documentServiceMock, string format = MediaTypeNames.Application.Json)
        {
            var formatResolverMock = new Mock<IFormatResolver>();
            var controller = new DocumentsController(documentServiceMock.Object, formatResolverMock.Object)
            {
                ControllerContext = new()
                {
                    HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext()
                }
            };
            formatResolverMock.Setup(f => f.GetPrefferedOutputFormat(It.IsAny<string>())).Returns((string s) => format);
            return controller;
        }
    }
}