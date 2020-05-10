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
    /// 常见路由约束
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Test1Controller : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">最大20</param>
        /// <returns></returns>
        [HttpGet("{id:max(20)}")]
        public bool Max([FromRoute]long id)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number">以三个数字开始</param>
        /// <returns></returns>
        [HttpGet("{number:regex(^\\d{{3}}$)}")]
        public bool Number(string number)
        {
            return true;
        }
    }
}