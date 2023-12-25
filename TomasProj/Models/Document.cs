using System.Xml.Linq;

using MessagePack;

namespace TomasProj.Models
{
    [MessagePackObject]
    public class Document
    {
        [Key(0)]
        public string Id { get; set; }
        [Key(1)]
        public List<string> Tags { get; set; } = new();
        [Key(2)]
        public Dictionary<string, string> Data { get; set; } = new();

        public void Update(Document updatedDocument)
        {
            Tags = updatedDocument.Tags;
            Data = updatedDocument.Data;
        }

        public XElement GetXML()
        {
            XElement r = new("Document");
            r.Add(new XElement(nameof(Id), Id));
            Tags.ForEach(tag => r.Add(new XElement(nameof(Tags), tag)));
            r.Add(new XElement(nameof(Data), Data.Select(d => new XElement(d.Key, d.Value))));
            return r;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Document document2)
            {
                return false;
            }

            return Id == document2.Id
                && Tags.Count == document2.Tags.Count
                && Tags.SequenceEqual(document2.Tags)
                && ((Data == null && document2.Data == null)
                    || Data.All(d1 => document2.Data.TryGetValue(d1.Key, out var value) && Equals(d1.Value, value)));
        }
    }
}
