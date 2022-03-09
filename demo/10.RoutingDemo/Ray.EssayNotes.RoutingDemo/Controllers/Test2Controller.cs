using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Ray.EssayNotes.RoutingDemo.Controllers
{
    /// <summary>
    /// 废弃的接口
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Test2Controller : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name:required}")]
        [Obsolete]
        public bool Reque(string name)
        {
            return true;
        }
    }
}