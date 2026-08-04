using parserAPI.Models;

namespace parserAPI.Interfaces
{
    public interface IParserInterface
    {
        ParserResponse Parse(ParserModel request);
    }
}
