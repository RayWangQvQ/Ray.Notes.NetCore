using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ray.EssayNotes.DDD.DiAutofacDemo.IServices;

namespace Ray.EssayNotes.DDD.DiAutofacDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Test03Controller : ControllerBase
    {
        private readonly IMyService _myService;

        public Test03Controller(IMyService myService)
        {
            this._myService = myService;
        }

        [HttpGet]
        public bool Get()
        {
            _myService.Test();
            return true;
        }
    }
}
