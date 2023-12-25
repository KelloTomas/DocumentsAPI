using TomasProj.Models;

namespace TomasProj.Services
{
    public static class DocumetStorageDataInit
    {
        public static Documents Documents
        {
            get
            {
                return new()
                {
                    new Document()
                    {
                        Id = "1",
                        Tags = { "important", ".NET" },
                        Data = new()
                        {
                            {"SomeData", "data" },
                            { "opt", "Fields" }
                        }
                    },
                    new Document()
                    {
                        Id = "2",
                        Tags = { "C#", "C++" },
                        Data = new()
                        {
                            {"SomeData", "new data" },
                            { "opt", "extraordinary" }
                        }
                    }
                };
            }
        }
    }
}
