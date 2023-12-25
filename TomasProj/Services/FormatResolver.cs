using System.Net.Mime;

namespace TomasProj.Services
{
    public class FormatResolver : IFormatResolver
    {
        public string GetPrefferedOutputFormat(string stringValues)
        {
            //string? stringValues = headerDictionary["Accept"].FirstOrDefault();
            return stringValues.Contains("json")
                    || stringValues.Contains("*/*")
                        ? MediaTypeNames.Application.Json
                    : stringValues.Contains("xml")
                        ? MediaTypeNames.Application.Xml
                    : string.Empty;
        }
    }
}
