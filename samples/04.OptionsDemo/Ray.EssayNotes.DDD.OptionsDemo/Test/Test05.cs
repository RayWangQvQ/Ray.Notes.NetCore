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
    [Description("结合配置系统-不具名")]
    public class Test05 : TestBase
    {
        protected override void InitConfiguration()
        {
            if (Program.ConfigurationRoot != null) return;
            Program.ConfigurationRoot = new ConfigurationBuilder()
                .AddJsonFile("order.json")
                .Build();
        }

        protected override void InitServiceProvider()
        {
            if (Program.ServiceProvider != null) return;
            Program.ServiceProvider = new ServiceCollection()
                .AddOptions()
                .Configure<OrderOption>(Program.ConfigurationRoot)
                .AddScoped<IOrderService, OrderService>()
                .BuildServiceProvider();
        }

        protected override void PrintServiceDescriptors()
        {
            //base.PrintServiceDescriptors();
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
            private readonly IOptions<OrderOption> _option1;
            private readonly IOptions<OrderOption> _option2;
            private readonly IOptionsSnapshot<OrderOption> _optionsSnapshot1;
            private readonly IOptionsSnapshot<OrderOption> _optionsSnapshot2;

            public OrderService(IOptions<OrderOption> option1, IOptions<OrderOption> option2,
                IOptionsSnapshot<OrderOption> optionsSnapshot1, IOptionsSnapshot<OrderOption> optionsSnapshot2)
            {
                _option1 = option1;
                _option2 = option2;
                _optionsSnapshot1 = optionsSnapshot1;
                _optionsSnapshot2 = optionsSnapshot2;
            }

            public void PrintOption()
            {
                Console.WriteLine($"_option1({_option1.GetHashCode()}):{_option1.AsFormatJsonStr()}");
                Console.WriteLine($"_option2({_option2.GetHashCode()}):{_option2.AsFormatJsonStr()}");
                Console.WriteLine($"_optionsSnapshot1({_optionsSnapshot1.GetHashCode()}):{_optionsSnapshot1.AsFormatJsonStr()}");
                Console.WriteLine($"_optionsSnapshot2({_optionsSnapshot2.GetHashCode()}):{_optionsSnapshot2.AsFormatJsonStr()}");
            }
        }
    }
}
