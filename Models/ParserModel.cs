using System.Net.Mime;

namespace parserAPI.Models
{
    public enum ContentType
    {
        CSV,
        Internal_JSON
    }
    public class ParserModel
    {
        public ContentType Type { get; set; }
        public string Content { get; set; } = string.Empty;
    }
    public class ParsedItem
    {
        public Dictionary<string, object?> Values { get; set; } = new();
    }
    public class ParserResponse
    {
        public string status { get; set; } = "";
        public int Count { get; set; }
        public List<ParsedItem> Items { get; set; } = new();
    }
}