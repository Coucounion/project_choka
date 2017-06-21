using Microsoft.AspNetCore.Mvc;
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
        private static string[] Summaries = new[]
        {
            "Freezing", "Chilly", "Cool", "Mild", "Balmy", "Hot", "Sweltering"
        };

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts(int startDateIndex)
        {
            var rng = new Random();
            var tmp = Enumerable.Range(1, MAX_DAYS).Select((x, bucket) => rng.Next(-20, 55)).ToArray();

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

            public string Summary
            {

                get
                {
                    int my = 0;
                    foreach (var band in Enumerable.Range(-20, 55).Batch(10))
                    {
                        if (band.Any(i => i == this.TemperatureC))
                            break;
                        //return Summaries[my];
                        my++;
                    }
                    return Summaries[my];
                }
            }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}

