using Microsoft.AspNetCore.Mvc;

namespace ProjectTracker.Controllers
{
    [Route("api/v1/[controller]")]
    public class IndexController : Controller
    {
        // GET
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("It works !");
        }
    }
}