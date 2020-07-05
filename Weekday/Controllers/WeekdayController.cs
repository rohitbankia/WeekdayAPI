using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Weekday.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeekdayController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeekdayController> _logger;

        public WeekdayController(ILogger<WeekdayController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Weekday> Get(string param = null)
        {
            return Enumerable.Range(0, 7).Select(index => new Weekday
            {
                dayOfWeek = $"{index}. {DateTime.Now.AddDays(index).DayOfWeek}",
                urlParamGiven = !string.IsNullOrEmpty(param),
                date = DateTime.Now.AddDays(index)
            })
            .ToArray();
        }

        [HttpPost]
        public IActionResult Post([FromBody]List<string> dates)
        {
            try
            {
                int len = dates.Count();
                if (len != 7)
                {
                    return BadRequest("Please send list of 7 dates only.");
                }
                List<Weekday> weekdays = new List<Weekday>();
                for (int i = 0; i < len; i++)
                {
                    DateTime dateTime = DateTime.Parse(dates[i]);
                    weekdays.Add(new Weekday
                    {
                        dayOfWeek = $"{i}. {dateTime.DayOfWeek}",
                        urlParamGiven = false,
                        date = dateTime
                    });
                }
                return Ok(weekdays);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.ToString());
                return BadRequest(exception);
            }
        }
    }
}
