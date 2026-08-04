using parserAPI.Models;

namespace parserAPI.Interfaces
{
    public interface IParserInterface
    {
        ParserResponse Parse(ParserModel request);
    }
    public interface IBase64Decoder
    {
        string Decode(string base64);
    }
}
