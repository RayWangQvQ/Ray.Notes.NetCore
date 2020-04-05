using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ray.EssayNotes.DDD.ScopeAndDisposableDemo.IServices;
using Newtonsoft.Json;
using Ray.Infrastructure.Extensions;

namespace Ray.EssayNotes.DDD.ScopeAndDisposableDemo.Controllers
{
    /// <summary>
    /// 测试自己new出的实例注册，在跟容器解析会不会被释放
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class Test04Controller : ControllerBase
    {
        private readonly IOrderService _orderService1;
        private readonly IOrderService _orderService2;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="orderService1"></param>
        /// <param name="orderService2"></param>
        public Test04Controller(IOrderService orderService1, IOrderService orderService2, IHostApplicationLifetime hostApplicationLifetime)
        {
            this._orderService1 = orderService1;
            this._orderService2 = orderService2;
            this._hostApplicationLifetime = hostApplicationLifetime;
        }

        [HttpGet]
        public bool Get()
        {
            Console.WriteLine($"_orderService1:{_orderService1.GetHashCode()}");
            Console.WriteLine($"_orderService2:{_orderService2.GetHashCode()}");

            using (var childScope = HttpContext.RequestServices.CreateScope())//RequestServices是当前请求所在的作用域
            {
                var orderService = childScope.ServiceProvider.GetService<IOrderService>();
                Console.WriteLine($"orderService:{orderService.GetHashCode()}");
            }

            Console.Write("根容器中可释放实例池：");
            var list = Startup.ServiceProviderRoot.GetDisposableCoponentNamesFromScope();
            Console.WriteLine(JsonConvert.SerializeObject(list).AsFormatJsonString());

            Console.WriteLine($"========请求结束=======");
            _hostApplicationLifetime.StopApplication();//使整个应用停止

            /* 停止后会释放根容器，此时new出来的实例并不会被释放
             * 原因：
             * 注册已实例化的对象到容器时，只是注册时讲实例添加到了实例池中，并不是添加到可释放实例池中（即是其实现了IDisposable接口）             
             */

            return true;
        }
    }
}