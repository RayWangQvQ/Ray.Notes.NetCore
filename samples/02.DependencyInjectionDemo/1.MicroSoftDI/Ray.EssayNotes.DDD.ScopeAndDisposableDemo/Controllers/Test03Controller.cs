using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Ray.EssayNotes.DDD.ScopeAndDisposableDemo.IServices;
using Ray.Infrastructure.Extensions;

namespace Ray.EssayNotes.DDD.ScopeAndDisposableDemo.Controllers
{
    /// <summary>
    /// 测试构造注入
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class Test03Controller : ControllerBase
    {
        private readonly IOrderService _orderService1;
        private readonly IOrderService _orderService2;

        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="orderService1"></param>
        /// <param name="orderService2"></param>
        public Test03Controller(IOrderService orderService1, IOrderService orderService2)
        {
            this._orderService1 = orderService1;
            this._orderService2 = orderService2;
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

            Console.WriteLine($"========请求结束=======");

            /* 请求结束，只会释放请求域，并不会释放根域
             * 所以这里不会取释放全局单例的实例
             */

            return true;
        }
    }
}