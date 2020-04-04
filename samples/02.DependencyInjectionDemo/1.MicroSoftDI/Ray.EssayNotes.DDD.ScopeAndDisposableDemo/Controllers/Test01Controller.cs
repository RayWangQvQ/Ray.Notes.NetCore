using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ray.EssayNotes.DDD.ScopeAndDisposableDemo.IServices;

namespace Ray.EssayNotes.DDD.ScopeAndDisposableDemo.Controllers
{
    /// <summary>
    /// 测试构造注入
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class Test01Controller : ControllerBase
    {
        private readonly IOrderService _orderService1;
        private readonly IOrderService _orderService2;

        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="orderService1"></param>
        /// <param name="orderService2"></param>
        public Test01Controller(IOrderService orderService1, IOrderService orderService2)
        {
            this._orderService1 = orderService1;
            this._orderService2 = orderService2;
        }

        [HttpGet]
        public bool Get()
        {
            Console.WriteLine($"_orderService1:{_orderService1.GetHashCode()}");
            Console.WriteLine($"_orderService2:{_orderService2.GetHashCode()}");

            Console.WriteLine($"========请求结束=======");

            return true;
        }
    }
}