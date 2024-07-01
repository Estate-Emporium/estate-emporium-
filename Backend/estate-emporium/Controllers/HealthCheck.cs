using estate_emporium.Models.db;
using estate_emporium.Services;
using Microsoft.AspNetCore.Mvc;

namespace estate_emporium.Controllers
{
  [Route("health")]
  [ApiExplorerSettings(IgnoreApi = true)] //used to hide from swagger, can be applied to entire controller ot jsut 1 endpoint
  public class HealthCheck(EstateDbContext dbContext, CertificateService certService) : Controller
  {
    private readonly EstateDbContext _dbContext = dbContext;
    private readonly CertificateService _certService = certService;

    [HttpGet]
    public IActionResult Index()
    {
      return new JsonResult("Health OK") { StatusCode = 200 };
    }

    [HttpGet]
    [Route("cert")]
    public async Task<IActionResult> TestCert()
    {
      try
      {
        var cert = await _certService.GetCertAndKey();
        if (cert != null)
        {
          return new ObjectResult(cert.ToString());
        }
        else return BadRequest("CERT FAIL");
      }
      catch
      {
        return BadRequest("CERT FAIL");
      }
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
