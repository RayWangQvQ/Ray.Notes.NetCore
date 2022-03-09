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

            service.PrintOption();
        }

        protected override void PrintServiceDescriptors()
        {
            //不打印
        }

        public class OrderService : IOrderService
        {
            private readonly OrderOption _option;

            public OrderService(OrderOption option)
            {
                this._option = option;
            }

            public void PrintOption()
            {
                Console.WriteLine(_option.AsFormatJsonStr());
            }
        }
    }
}
