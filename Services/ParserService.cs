using parserAPI.Interfaces;
using parserAPI.Models;
using System.Text;
using System.Text.Json;

namespace parserAPI.Services
{
    public class Base64Decoder : IBase64DecoderInterface
    {
        public string Decode(string base64)
        {
            try
            {
                var bytes = Convert.FromBase64String(base64);
                return Encoding.UTF8.GetString(bytes);
            }
            catch (FormatException)
            {
                throw new ArgumentException("Invalid Base64 string.");
            }
        }
    }
    public class ParserService : IParserInterface
    {
        private readonly IBase64DecoderInterface _decoder;
        public ParserService(IBase64DecoderInterface decoder)
        {
            _decoder = decoder;
        }
        public ParserResponse Parse(ParserModel request)
        {
            var decoder = _decoder.Decode(request.Content);
            return request.Type switch
            {
                ContentType.CSV => ParseCSV(decoder),
                ContentType.Internal_JSON => ParseInternalJSON(decoder),
                _ => throw new NotSupportedException($"Content type {request.Type} is not supported.")
            };

        }
        private ParserResponse ParseCSV(string csv)
        {
            var lines = csv.Split("\n", StringSplitOptions.RemoveEmptyEntries);

            if (lines.Length == 0)
            {
                throw new Exception("CSV content too short to parse.");
            }
            var headers = lines[0].Split(',');
            var response = new ParserResponse
            {
                status = "success",
            };
            foreach (var line in lines.Skip(1))
            {
                var values = line.Split(',');
                var item = new ParsedItem();
                for (int i = 0; i < headers.Length; i++)
                {
                    if (values.Length > i)
                    {
                        item.Values[headers[i]] = values[i];
                    }
                    else
                    {
                        item.Values[headers[i]] = null;
                    }
                }
                response.Items.Add(item);
            }
            response.Count = response.Items.Count;
            return response;
        }
        private ParserResponse ParseInternalJSON(string json)
        {
            JsonDocument document;
            try
            {
                document = JsonDocument.Parse(json);

            }
            catch (JsonException)
            {
                throw new Exception("Invalid JSON format.");
            }
            var response = new ParserResponse();
            if (document.RootElement.ValueKind == JsonValueKind.Array)
            {

                foreach (var element in document.RootElement.EnumerateArray())
                {
                    var item = new ParsedItem();

                    foreach (var property in element.EnumerateObject())
                    {
                        item.Values[property.Name] = property.Value.ToString();
                    }
                    response.Items.Add(item);
                }

            }
            else
            {
                var item = new ParsedItem();
                foreach (var property in document.RootElement.EnumerateObject())
                {
                    item.Values[property.Name] = property.Value.ToString();
                }
                response.Items.Add(item);
            }
            response.status = "success";
            response.Count = response.Items.Count;
            return response;

        }
    }
}