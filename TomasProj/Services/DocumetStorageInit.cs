using TomasProj.Models;

namespace TomasProj.Services
{
    public class DocumetStorageInit : IDocumentStorageInit
    {
        public Documents Documents => DocumetStorageDataInit.Documents;
    }
}
