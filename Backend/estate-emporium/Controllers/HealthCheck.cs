using estate_emporium.Models;
using Microsoft.AspNetCore.Mvc;

namespace estate_emporium.Controllers
{
  [Route("health")]
  [ApiExplorerSettings(IgnoreApi = true)] //used to hide from swagger, can be applied to entire controller ot jsut 1 endpoint
  public class HealthCheck(EstateDbContext dbContext) : Controller
  {
    private readonly EstateDbContext _dbContext = dbContext;
    [HttpGet]
    public IActionResult Index()
    {
      return new JsonResult("Health OK") { StatusCode = 200 };
    }
    [HttpGet]
    [Route("env")]
    public IActionResult TestEnv()
    {
      var testEnv = System.Environment.GetEnvironmentVariable("ENV_WORKING") ?? "FALSE";
      return new ObjectResult(testEnv);
    }

    [HttpGet]
    [Route("db")]
    public IActionResult TestDb()
    {
        try
        {
            var test = _dbContext.Statuses.Select(x => x.StatusName).ToList();
            return new ObjectResult(test);
        }
        catch
        {
            return BadRequest("DB CONNECT FAIL");
        }

    }
    }
}
