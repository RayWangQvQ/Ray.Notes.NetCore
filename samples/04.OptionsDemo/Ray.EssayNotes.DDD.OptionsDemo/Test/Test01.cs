using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Ray.EssayNotes.DDD.OptionsDemo.Test
{
    [Description("Options选项模式的应用场景")]
    public class Test01 : TestBase
    {
        protected override void InitConfiguration()
        {
            //不使用配置系统
        }

        protected override void InitServiceProvider()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<OrderOption>();//注册选项
            serviceCollection.AddSingleton<IOrderService, OrderService>();//注册服务

            Program.ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        protected override void Print()
        {
            var service = Program.ServiceProvider.GetRequiredService<IOrderService>();

            Console.WriteLine($"最大订单数：{service.GetMaxNum()}");
        }

        protected override void PrintServiceDescriptors()
        {
            //base.PrintServiceDescriptors();
        }

        public class OrderService : IOrderService
        {
            private readonly OrderOption _option;

            public OrderService(OrderOption option)
            {
                this._option = option;
            }
            public int GetMaxNum()
            {
                return _option.MaxOrderNum;
            }
        }
    }
}
