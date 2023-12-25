using System.Net.Mime;

using Microsoft.AspNetCore.Mvc;

using Moq;

using TomasProj.Controllers;
using TomasProj.Interfaces;
using TomasProj.Models;
using TomasProj.Services;

namespace TomasProjXUnit
{
    public class DocumentsControllerTest
    {
        [Fact]
        public void GetAll_ReturnsOk()
        {
            // Arrange
            var documentServiceMock = new Mock<IDocumentStorage>();
            var expectedDocuments = DocumetStorageDataInit.Documents;

            documentServiceMock.Setup(mock => mock.GetAll()).Returns(expectedDocuments);
            DocumentsController controller = GetController(documentServiceMock);

            // Act
            var result = controller.GetAll() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var actualDocuments = Assert.IsAssignableFrom<IEnumerable<Document>>(result.Value);
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
            var result = controller.GetById(testId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var actualDocument = Assert.IsAssignableFrom<Document>(result.Value);
            Assert.Equal(actualDocument.Id, testId);
        }

        private static DocumentsController GetController(Mock<IDocumentStorage> documentServiceMock)
        {
            var controller = new DocumentsController(documentServiceMock.Object)
            {
                ControllerContext = new()
                {
                    HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext()
                }
            };
            return controller;
        }
    }
}