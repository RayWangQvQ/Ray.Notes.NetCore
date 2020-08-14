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
    [Description("三种Options及生命周期：IOptions、IOptionsSnapshot、IOptionsMonitor")]
    public class Test03 : TestBase
    {
        protected override void InitConfiguration()
        {
            //不使用配置系统
        }

        protected override void InitServiceProvider()
        {
            if (Program.ServiceProvider != null) return;

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddOptions();
            serviceCollection.Configure<OrderOption>(it => { it.MaxOrderNum = 10; });

            serviceCollection.AddScoped<IOrderService, OrderService>();
            Program.ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        protected override void PrintServiceDescriptors()
        {
            base.PrintServiceDescriptors();

            /*
             * IOptions与IOptionsMonitor为全局单例的
             * IOptionsSnapshot为域内单例的
             */
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
            }

            public void PrintOption()
            {
                Console.WriteLine($"_option1({_option1.GetHashCode()}):{_option1.AsFormatJsonStr()}");
                Console.WriteLine($"_option2({_option2.GetHashCode()}):{_option2.AsFormatJsonStr()}");

                Console.WriteLine($"_optionsSnapshot1({_optionsSnapshot1.GetHashCode()}):{_optionsSnapshot1.AsFormatJsonStr()}");
                Console.WriteLine($"_optionsSnapshot2({_optionsSnapshot2.GetHashCode()}):{_optionsSnapshot2.AsFormatJsonStr()}");

                Console.WriteLine($"_optionsMonitor1({_optionsMonitor1.GetHashCode()}):{_optionsMonitor1.AsFormatJsonStr()}");
                Console.WriteLine($"_optionsMonitor2({_optionsMonitor2.GetHashCode()}):{_optionsMonitor2.AsFormatJsonStr()}");

                PrintOptionCatch();
            }

            /// <summary>
            /// 打印内部封装的缓存
            /// </summary>
            private void PrintOptionCatch()
            {
                var catch1 = _option1.GetFieldValue("_cache").GetFieldValue("_cache");
                Console.WriteLine($"option1缓存（{catch1.GetHashCode()}）：{catch1.AsFormatJsonStr()}");

                var catch2 = _optionsSnapshot1.GetFieldValue("_cache").GetFieldValue("_cache");
                Console.WriteLine($"option2缓存（{catch2.GetHashCode()}）：{catch2.AsFormatJsonStr()}");

                var catch3 = _optionsMonitor1.GetFieldValue("_cache").GetFieldValue("_cache");
                Console.WriteLine($"optionsMonitor1缓存（{catch3.GetHashCode()}）：{catch3.AsFormatJsonStr()}");
            }
        }
    }
}
