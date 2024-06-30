using estate_emporium.Models.db;
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
    [Route("db")]
    public IActionResult TestDb()
    {
      try
      {
        var test = _dbContext.Statuses.Select(x => x.StatusName).ToList();
        if (test.Any())
        {
          return new ObjectResult(test);
        }
        else return BadRequest("USING IN MEMORY DB");
      }
      catch
      {
        return BadRequest("DB CONNECT FAIL");
      }

    }
  }
}
