using System.Xml.Linq;

namespace TomasProj.Models
{
    public class Documents : List<Document>
    {
        public XElement GetDocumentsAsXElement()
        {
            XElement documentsXElement = new("Documents");

            foreach (Document document in this)
            {
                documentsXElement.Add(document.GetXML());
            }

            return documentsXElement;
        }
    }
}
