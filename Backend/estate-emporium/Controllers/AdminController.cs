using estate_emporium.Models.db;
using estate_emporium.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace estate_emporium.Controllers
{
  [Route("admin")]
  public class AdminController(DbService dbService) : Controller
  {
    private readonly DbService _dbService = dbService;

    /// <summary>
    /// do not use pLEASE
    /// </summary>
    /// <returns>A response indicating the result.</returns>
    /// 
    [Authorize]
    [HttpGet("get")]
    public IActionResult Index()
    {
      var data = _dbService.getFrontData();
      return new JsonResult(data) { StatusCode = 200 };
    }
  }
}
