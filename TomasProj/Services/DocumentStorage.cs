using TomasProj.Interfaces;
using TomasProj.Models;

namespace TomasProj.Services
{
    public class DocumentStorage : IDocumentStorage
    {
        private readonly Documents _documents;

        public DocumentStorage(IDocumentStorageInit storage)
        {
            _documents = storage.Documents;
        }

        public Document? GetById(string id)
            => _documents.FirstOrDefault(d => d.Id == id);

        public IEnumerable<Document> GetAll()
            => _documents;

        public bool Update(Document document)
        {
            var existingDoc = GetById(document.Id);
            if (existingDoc != null)
            {
                existingDoc.Update(document);
                return true;
            }

            return false;
        }

        public void AddOrUpdate(Document document)
        {
            if (!Update(document))
            {
                _documents.Add(document);
            }
        }
    }
}
