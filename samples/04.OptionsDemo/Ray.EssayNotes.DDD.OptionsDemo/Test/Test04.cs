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
    [Description("具名Options与非具名Options")]
    public class Test04 : TestBase
    {
        protected override void InitConfiguration()
        {
            //不使用配置
        }

        protected override void InitServiceProvider()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.Configure<OrderOption>("foo", it => { it.MaxOrderNum = 9; });
            serviceCollection.AddOptions<OrderOption>("bar").Configure(it => { it.MaxOrderNum = 900; });

            serviceCollection.AddScoped<IOrderService, OrderService>();

            Program.ServiceProvider = serviceCollection.BuildServiceProvider();
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
                PrintOptionOne();
                PrintOptionTwo();
            }

            /// <summary>
            /// 直接序列化3中Options
            /// </summary>
            public void PrintOptionOne()
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
            /// 使用具名的IOptionsMonitor和IOptionsSnapshot的Get方法读取指定名称的Options
            /// </summary>
            public void PrintOptionTwo()
            {
                Console.WriteLine($"_optionsSnapshot1.foo:{_optionsSnapshot1.Get("foo").AsFormatJsonStr()}");
                Console.WriteLine($"_optionsSnapshot1.bar:{_optionsSnapshot1.Get("bar").AsFormatJsonStr()}");

                Console.WriteLine($"_optionsMonitor1.foo:{_optionsMonitor1.Get("foo").AsFormatJsonStr()}");
                Console.WriteLine($"_optionsMonitor2.bar:{_optionsMonitor2.Get("bar").AsFormatJsonStr()}");

                PrintOptionCatch();
            }

            /// <summary>
            /// 打印内部封装的缓存池
            /// </summary>
            private void PrintOptionCatch()
            {
                var catch1 = _option1.GetFieldValue("_cache").GetFieldValue("_cache");
                Console.WriteLine($"option1缓存（{catch1.GetHashCode()}）：{catch1.AsFormatJsonStr()}");

                var catch2 = _optionsSnapshot1.GetFieldValue("_cache").GetFieldValue("_cache");
                Console.WriteLine($"optionsSnapshot1缓存（{catch2.GetHashCode()}）：{catch2.AsFormatJsonStr()}");

                var catch3 = _optionsMonitor1.GetFieldValue("_cache").GetFieldValue("_cache");
                Console.WriteLine($"optionsMonitor1缓存（{catch3.GetHashCode()}）：{catch3.AsFormatJsonStr()}");
            }
        }
    }
}
