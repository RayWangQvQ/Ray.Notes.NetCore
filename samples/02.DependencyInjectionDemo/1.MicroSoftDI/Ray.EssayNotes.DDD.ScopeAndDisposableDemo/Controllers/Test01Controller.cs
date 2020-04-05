﻿using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Ray.EssayNotes.DDD.ScopeAndDisposableDemo.IServices;
using Ray.Infrastructure.Extensions;

namespace Ray.EssayNotes.DDD.ScopeAndDisposableDemo.Controllers
{
    /// <summary>
    /// 测试瞬时实例的释放
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class Test01Controller : MyControllerBase
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

            this.PrintFromRequestServiceScope();

            Console.WriteLine($"========请求结束=======");

            return true;
        }
    }
}