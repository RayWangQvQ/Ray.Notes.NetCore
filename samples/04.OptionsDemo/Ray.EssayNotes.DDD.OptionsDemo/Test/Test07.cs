using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace Ray.EssayNotes.DDD.OptionsDemo.Test
{
    [Description("数据源变更刷新-非具名Options")]
    public class Test07 : TestBase
    {
        protected override void InitConfiguration()
        {
            if (Program.ConfigurationRoot != null) return;
            Program.ConfigurationRoot = new ConfigurationBuilder()
                .AddJsonFile("order.json", true, true)//开启配置数据源变更通知
                .Build();
            ChangeToken.OnChange(() => Program.ConfigurationRoot.GetReloadToken(), () =>
            {
                Console.WriteLine("触发配置变更");
            });
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
            private readonly IOptionsMonitor<OrderOption> _optionsMonitor1;
            private readonly IOptionsMonitor<OrderOption> _optionsMonitor2;

            public OrderService(IOptions<OrderOption> option1, IOptions<OrderOption> option2,
                IOptionsSnapshot<OrderOption> optionsSnapshot1, IOptionsSnapshot<OrderOption> optionsSnapshot2,
                IOptionsMonitor<OrderOption> optionsMonitor1, IOptionsMonitor<OrderOption> optionsMonitor2)
            {
                _option1 = option1;
                _option2 = option2;
                _optionsSnapshot1 = optionsSnapshot1;
                _optionsSnapshot2 = optionsSnapshot2;
                _optionsMonitor1 = optionsMonitor1;
                _optionsMonitor2 = optionsMonitor2;
                _optionsMonitor1.OnChange(x => Console.WriteLine($"_optionsMonitor1变更：{_optionsMonitor1.AsFormatJsonStr()}"));
                _optionsMonitor2.OnChange(x => Console.WriteLine($"_optionsMonitor2变更：{_optionsMonitor2.AsFormatJsonStr()}"));
            }

            public void PrintOption()
            {
                Console.WriteLine($"_option1({_option1.GetHashCode()}):{_option1.AsFormatJsonStr()}");
                Console.WriteLine($"_option2({_option2.GetHashCode()}):{_option2.AsFormatJsonStr()}");

                Console.WriteLine($"_optionsMonitor1({_optionsMonitor1.GetHashCode()}):{_optionsMonitor1.AsFormatJsonStr()}");
                Console.WriteLine($"_optionsMonitor2({_optionsMonitor2.GetHashCode()}):{_optionsMonitor2.AsFormatJsonStr()}");

                Console.WriteLine($"_optionsSnapshot1({_optionsSnapshot1.GetHashCode()}):{_optionsSnapshot1.AsFormatJsonStr()}");
                Console.WriteLine($"_optionsSnapshot2({_optionsSnapshot2.GetHashCode()}):{_optionsSnapshot2.AsFormatJsonStr()}");
            }
        }
    }
}
