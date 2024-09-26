using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

public static class PrimeFunction
{
    [FunctionName("PrimeFunction")]
    public static IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "prime")] HttpRequest req)
    {
        string limitQuery = req.Query["limit"];
        if (int.TryParse(limitQuery, out int limit) && limit <= 10000)
        {
            var primes = SieveOfEratosthenes(limit);
            return new OkObjectResult(new { primes });
        }
        return new BadRequestObjectResult("Please pass a valid limit (<= 10000) on the query string");
    }

    private static List<int> SieveOfEratosthenes(int limit)
    {
        bool[] isPrime = new bool[limit + 1];
        for (int i = 2; i <= limit; i++) isPrime[i] = true;

        for (int i = 2; i * i <= limit; i++)
        {
            if (isPrime[i])
            {
                for (int j = i * i; j <= limit; j += i)
                    isPrime[j] = false;
            }
        }

        var primes = new List<int>();
        for (int i = 2; i <= limit; i++)
        {
            if (isPrime[i]) primes.Add(i);
        }
        return primes;
    }
}
