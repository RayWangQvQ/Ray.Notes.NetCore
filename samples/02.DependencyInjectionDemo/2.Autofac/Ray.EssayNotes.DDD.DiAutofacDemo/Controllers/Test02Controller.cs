using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ray.EssayNotes.DDD.DiAutofacDemo.IServices;
using System.Text.Json;
using Ray.Infrastructure.Extensions;
using Ray.EssayNotes.DDD.DiAutofacDemo.Services;

namespace Ray.EssayNotes.DDD.DiAutofacDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Test02Controller : ControllerBase
    {
        private readonly MyService _myService;

        public Test02Controller(MyService myService)
        {
            this._myService = myService;
        }

        [HttpGet]
        public bool Get()
        {
            Console.WriteLine($"{JsonSerializer.Serialize(_myService)}");
            return true;
        }
    }
}
