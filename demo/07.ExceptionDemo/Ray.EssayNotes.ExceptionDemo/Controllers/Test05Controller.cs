using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ray.EssayNotes.ExceptionDemo.Exceptions;

namespace Ray.EssayNotes.ExceptionDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [MyExceptionFilter]//添加异常过滤器特性
    public class Test05Controller : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public Test05Controller(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        public IEnumerable<WeatherForecast> Get(int id)
        {
            if (id == 0)
                throw new Exception("报个错");
            else
                throw new MyServerException("服务出错了", 65);
        }
    }
}
