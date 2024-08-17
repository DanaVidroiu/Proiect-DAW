using Microsoft.AspNetCore.Mvc;
using LearningPlatform.Models;

namespace LearningPlatform.Controllers
{
    public class DatabaseTestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DatabaseTestController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("database-test")]
        public IActionResult Test()
        {
            // Verificăm dacă baza de date poate fi accesată
            var isDatabaseAccessible = _context.Database.CanConnect();

            return Content($"Database connection successful: {isDatabaseAccessible}");
        }
    }
}
