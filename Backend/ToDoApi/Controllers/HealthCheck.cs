﻿using Microsoft.AspNetCore.Mvc;

namespace ToDoApi.Controllers
{
    [Route("health")]
    [ApiExplorerSettings(IgnoreApi = true)] //used to hide from swagger, can be applied to entire controller ot jsut 1 endpoint
    public class HealthCheck : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return new JsonResult("Health OK");
        }
        [HttpGet]
        [Route("env")]
        public IActionResult TestEnv()
        {
            var testEnv = System.Environment.GetEnvironmentVariable("ENV_WORKING") ?? "FALSE";
            return new ObjectResult(testEnv);
        }
    }
}
