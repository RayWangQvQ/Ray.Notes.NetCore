using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ray.EssayNotes.DirectoryDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWebHostEnvironment _env;

        public TestController(ILogger<WeatherForecastController> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        [HttpGet]
        public Dictionary<string, string> Get()
        {
            System.IO.File.WriteAllTextAsync("./Test.txt", "Hello");//当前类文件所在的目录

            return new Dictionary<string, string>
            {
                {"Directory.GetCurrentDirectory()", Directory.GetCurrentDirectory()},//当前类文件所在的目录
                { "AppDomain.CurrentDomain.BaseDirectory",AppDomain.CurrentDomain.BaseDirectory},//当前执行程序集（dll或exe）所在目录
                { "IWebHostEnvironment",_env.AsJsonStr()}
            };
        }
    }
}
