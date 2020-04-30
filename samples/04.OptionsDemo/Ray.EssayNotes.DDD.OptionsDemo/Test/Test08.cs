using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Ray.EssayNotes.DDD.OptionsDemo.Test
{
    [Description("数据源变更刷新-具名的Options")]
    public class Test08 : TestBase
    {
        protected override void InitConfiguration()
        {
            if (Program.ConfigurationRoot != null) return;
            Program.ConfigurationRoot = new ConfigurationBuilder()
                .AddJsonFile("orders.json", true, true)
                .Build();
        }

        protected override void InitServiceProvider()
        {
            if (Program.ServiceProvider != null) return;
            Program.ServiceProvider = new ServiceCollection()
                .Configure<OrderOption>("foo", Program.ConfigurationRoot.GetSection("foo"))
                .Configure<OrderOption>("bar", Program.ConfigurationRoot.GetSection("bar"))
                .AddScoped<IOrderService, OrderService>()
                .BuildServiceProvider();
        }

        protected override void Print()
        {
            using (var childScope = Program.ServiceProvider.CreateScope())
            {
                var service = childScope.ServiceProvider.GetRequiredService<IOrderService>();

                service.PrintOption();
            }
        }


        public class OrderService : IOrderService
        {
            private readonly IOptionsMonitor<OrderOption> _optionsMonitor1;
            private readonly IOptionsMonitor<OrderOption> _optionsMonitor2;

            public OrderService(IOptionsMonitor<OrderOption> optionsMonitor1, IOptionsMonitor<OrderOption> optionsMonitor2)
            {
                _optionsMonitor1 = optionsMonitor1;
                _optionsMonitor2 = optionsMonitor2;
                _optionsMonitor1.OnChange(x => Console.WriteLine($"_optionsMonitor1变更：{_optionsMonitor1.AsFormatJsonStr()}"));
                _optionsMonitor2.OnChange(x => Console.WriteLine($"_optionsMonitor2变更：{_optionsMonitor2.AsFormatJsonStr()}"));
                //这里并不是只监听到变更的，而是都会进来，即foo进一次bar进一次
            }

            public void PrintOption()
            {
                Console.WriteLine($"_optionsMonitor1({_optionsMonitor1.GetHashCode()}):{_optionsMonitor1.AsFormatJsonStr()}");
                Console.WriteLine($"_optionsMonitor2({_optionsMonitor2.GetHashCode()}):{_optionsMonitor2.AsFormatJsonStr()}");

                Console.WriteLine($"_optionsMonitor1.foo:{_optionsMonitor1.Get("foo").AsFormatJsonStr()}");
            }
        }
    }
}
