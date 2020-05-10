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
    /// 生成链接
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Test4Controller : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="linkGenerator"></param>
        /// <returns></returns>
        [HttpGet()]
        public List<string> Test([FromServices]LinkGenerator linkGenerator)
        {
            string path = linkGenerator.GetPathByAction(HttpContext,
                action: "Max",
                controller: "Test1",
                values: new { id = 1 });

            string uri = linkGenerator.GetUriByAction(HttpContext,
                action: "Max",
                controller: "Test1",
                values: new { id = 1 });

            return new List<string> { path, uri };
        }
    }
}