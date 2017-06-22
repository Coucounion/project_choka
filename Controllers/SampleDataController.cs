using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplicationBasic.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        const int MAX_DAYS = 7;
        private const int MIN_RANGE_TEMP = -20;
        private const int MAX_RANGE_TEMP = 55;
        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private static readonly List<int> tempList = Enumerable.Range(MIN_RANGE_TEMP, MAX_RANGE_TEMP - MIN_RANGE_TEMP).ToList();
        private static readonly int BATCH_SIZE =
            Convert.ToInt32(Math.Ceiling((MAX_RANGE_TEMP - MIN_RANGE_TEMP) / (decimal)Summaries.Length));

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts(int startDateIndex)
        {
            var rng = new Random();
            var tmp = Enumerable.Range(1, MAX_DAYS).Select((x, bucket) => rng.Next(MIN_RANGE_TEMP, MAX_RANGE_TEMP)).ToArray();

            return tmp.AsEnumerable().Select((x, idx) => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(idx + startDateIndex).ToString("d"),
                TemperatureC = tmp[idx]
            }).AsEnumerable();

        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public int TemperatureF
            {
                get { return 32 + (int)(TemperatureC / 0.5556); }
            }
            public string Summary
            {
                get
                {
                    string summary = null;
                    tempList.Batch(BATCH_SIZE)
                        .Select((pr, idx) => new { Index = idx, BatchColl = pr }).AsEnumerable()
                        .ForEach((band) =>
                        {
                            if (band.BatchColl.Any(i => i == TemperatureC))
                                summary = Summaries[band.Index];
                        });

                    return summary;
                }
            }
        }
    }
}

