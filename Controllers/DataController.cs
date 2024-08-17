using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;
using System.IO;

namespace LearningPlatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        // Metoda care returnează XML
        [HttpGet("xml-data")]
        public IActionResult GetXmlData()
        {
            var data = new ExampleData { Name = "Example" };
            var xml = new XmlSerializer(data.GetType());
            var stringWriter = new StringWriter();
            xml.Serialize(stringWriter, data);
            return Content(stringWriter.ToString(), "text/xml");
        }

        // Metoda care returnează JSON
        [HttpGet("data")]
        public IActionResult GetData()
        {
            var data = new { Name = "Example" };
            return Ok(data); // Returnează un obiect valid în format JSON
        }

        // Endpoint-ul pentru returnarea unui răspuns gol
        [HttpGet("empty")]
        public IActionResult GetEmpty()
        {
            return NoContent(); // Răspuns gol, status code 204
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return Content("Welcome to the API! Use specific endpoints to get data.");
        }
    }

    public class ExampleData
    {
        public string? Name { get; set; }
    }
}
