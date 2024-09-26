using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

public static class NumberFunction
{
    [FunctionName("NumberFunction")]
    public static IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "number")] HttpRequest req)
    {
        string nQuery = req.Query["n"];
        if (int.TryParse(nQuery, out int n) && n >= 0 && n <= 50)
        {
            long number = Fibonacci(n);
            return new OkObjectResult(new { number });
        }
        return new BadRequestObjectResult("Please pass a valid n (0-50) on the query string");
    }

    private static long Fibonacci(int n)
    {
        if (n <= 1) return n;
        long a = 0, b = 1;
        for (int i = 2; i <= n; i++)
        {
            long temp = a;
            a = b;
            b = temp + b;
        }
        return b;
    }
}
