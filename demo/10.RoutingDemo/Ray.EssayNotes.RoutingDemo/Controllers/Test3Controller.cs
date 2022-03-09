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
    /// 自定义路由约束
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Test3Controller : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">必须可以转为long</param>
        /// <returns></returns>
        [HttpGet("{id:isLong}")]
        public bool OrderExist([FromRoute]string id)
        {
            return true;
        }
    }
}