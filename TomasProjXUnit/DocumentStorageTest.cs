using Moq;

using TomasProj.Models;
using TomasProj.Services;

namespace TomasProjXUnit
{
    public class DocumentStorageTest
    {
        [Fact]
        public void GetAll_ReturnsNoDocuments()
        {
            // Arrange
            var documentStorageInitMock = new Mock<IDocumentStorageInit>();
            documentStorageInitMock.Setup(o => o.Documents).Returns(new Documents());
            var documentStorage = new DocumentStorage(documentStorageInitMock.Object);

            // Act

            // Assert
            var retrievedDocument = documentStorage.GetAll();
            Assert.Empty(retrievedDocument);
        }

        [Fact]
        public void GetAll_ReturnsAllDocuments()
        {
            // Arrange
            var documentStorageInitMock = new Mock<IDocumentStorageInit>();
            documentStorageInitMock.Setup(o => o.Documents).Returns(DocumetStorageDataInit.Documents);
            var documentStorage = new DocumentStorage(documentStorageInitMock.Object);

            // Act

            // Assert
            var retrievedDocument = documentStorage.GetAll();
            Assert.NotEmpty(retrievedDocument);
        }

        [Fact]
        public void AddOrUpdate_AddsNewDocument()
        {
            // Arrange
            string testId = "new uniqe id 1";
            var documentStorageInitMock = new Mock<IDocumentStorageInit>();
            documentStorageInitMock.Setup(o => o.Documents).Returns(new Documents());
            var documentStorage = new DocumentStorage(documentStorageInitMock.Object);
            var newDocument = new Document
            {
                Id = testId,
                Tags = new List<string> { "tag1" },
                Data = new Dictionary<string, string> { { "Key", "Value" } }
            };

            // Act
            documentStorage.AddOrUpdate(newDocument);

            // Assert
            var retrievedDocument = documentStorage.GetById(testId);
            Assert.NotNull(retrievedDocument);
            Assert.Equal(newDocument, retrievedDocument);
            Assert.Single(documentStorage.GetAll());
        }

        [Fact]
        public void AddOrUpdate_UpdatesExistingDocument()
        {
            // Arrange
            var documentStorageInitMock = new Mock<IDocumentStorageInit>();
            documentStorageInitMock.Setup(o => o.Documents).Returns(DocumetStorageDataInit.Documents);
            var documentStorage = new DocumentStorage(documentStorageInitMock.Object);
            var documentToUpdate = DocumetStorageDataInit.Documents.First();
            var updatedDocument = new Document
            {
                Id = documentToUpdate.Id,
                Tags = new List<string> { "new tag" },
                Data = new Dictionary<string, string> { { "UpdatedKey", "UpdatedValue" } }
            };

            // Act
            documentStorage.AddOrUpdate(updatedDocument);

            // Assert
            var retrievedDocument = documentStorage.GetById(documentToUpdate.Id);
            Assert.NotNull(retrievedDocument);
            Assert.Equal(updatedDocument, retrievedDocument);
        }

        [Fact]
        public void GetById_ReturnsNullForNonexistentDocument()
        {
            // Arrange
            var documentStorageInitMock = new Mock<IDocumentStorageInit>();
            documentStorageInitMock.Setup(o => o.Documents).Returns(DocumetStorageDataInit.Documents);
            var documentStorage = new DocumentStorage(documentStorageInitMock.Object);

            // Act
            var retrievedDocument = documentStorage.GetById("nonexistentId");

            // Assert
            Assert.Null(retrievedDocument);
        }
    }
}