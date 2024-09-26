using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

public static class TempFunction
{
    [FunctionName("TempFunction")]
    public static IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "temp")] HttpRequest req)
    {
        string celsiusQuery = req.Query["celsius"];
        if (double.TryParse(celsiusQuery, out double celsius))
        {
            double kelvin = celsius + 273.15;
            var result = new { celsius, kelvin };
            return new OkObjectResult(result);
        }
        return new BadRequestObjectResult("Please pass a valid celsius value on the query string");
    }
}
