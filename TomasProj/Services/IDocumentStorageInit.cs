using TomasProj.Models;

namespace TomasProj.Services
{
    public interface IDocumentStorageInit
    {
        List<Document> Documents { get; }
    }
}
