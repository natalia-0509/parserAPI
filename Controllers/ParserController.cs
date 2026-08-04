using Microsoft.AspNetCore.Mvc;
using parserAPI.Interfaces;
using parserAPI.Models;

namespace parserAPI.Controllers
{
    [ApiController]
    [Route("/api/v1")]
    public class ParserController : ControllerBase
    {
        private readonly IParserInterface _parserService;
        public ParserController(IParserInterface parserService)
        {
            _parserService = parserService;
        }
        [HttpPost("parse-content")]
        [Consumes ("application/json")]
        public IActionResult Parse([FromBody] ParserModel request)
        {
            try
            {
                var result = _parserService.Parse(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }
    }
}
