using TomasProj.Models;

namespace TomasProj.Interfaces
{
    public interface IDocumentStorage
    {
        bool Update(Document document);
        void AddOrUpdate(Document document);
        IEnumerable<Document> GetAll();
        Document? GetById(string id);
    }
}