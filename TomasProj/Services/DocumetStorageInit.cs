using TomasProj.Models;

namespace TomasProj.Services
{
    public class DocumetStorageInit : IDocumentStorageInit
    {
        public List<Document> Documents => DocumetStorageDataInit.Documents;
    }
}
