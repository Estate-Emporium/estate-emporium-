using Microsoft.AspNetCore.Mvc;

namespace ToDoApi.Controllers
{
    [Route("health")]
    public class HealthCheck : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return new ObjectResult("Health OK");
        }
    }
}
