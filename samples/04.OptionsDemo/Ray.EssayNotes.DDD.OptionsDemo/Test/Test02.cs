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
    [Description("基础用法（不使用配置框架）-注册非具名Options")]
    public class Test02 : TestBase
    {
        protected override void InitConfiguration()
        {
            //不使用配置系统
        }

        protected override void InitServiceProvider()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddOptions();
            serviceCollection.Configure<OrderOption>(it =>
            {
                it.MaxOrderNum = 10;
            });
            serviceCollection.AddSingleton<IOrderService, OrderService>();

            Program.ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        protected override void Print()
        {
            var service = Program.ServiceProvider.GetRequiredService<IOrderService>();

            service.PrintOption();
        }

        public class OrderService : IOrderService
        {
            private readonly IOptions<OrderOption> _option;

            public OrderService(IOptions<OrderOption> option)
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
