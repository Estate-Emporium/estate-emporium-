using estate_emporium.Models;
using estate_emporium.Models.HomeLoans;
using estate_emporium.Models.Zeus;
using estate_emporium.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
namespace estate_emporium.Controllers;

[Route("zeus")]
public class ZeusController(DbService dbService, StockService stockService) : Controller
{
  DbService _dbservice = dbService;
  StockService _stockservice = stockService;

  [HttpPost("reset")]
  public async Task<IActionResult> ProcessAction([FromBody] ActionRequest request)
  {
    if (request == null)
    {
      return BadRequest("Invalid request body.");
    }

    if (request.Action == "start")
    {
      // Logic for handling "start" action
      await _stockservice.registerStockAsync();
      return Ok($"Started something at {request.StartTime}...");
    }
    else if (request.Action == "reset")
    {
      // Logic for handling "reset" action
      _dbservice.resetData();
      return Ok($"Reset something at {request.StartTime}...");
    }
    else
    {
      // Handle unknown actions
      return BadRequest("Unknown action.");
    }
  }
}


