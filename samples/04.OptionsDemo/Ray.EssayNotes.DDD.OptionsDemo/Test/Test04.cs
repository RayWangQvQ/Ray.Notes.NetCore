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
    [Description("基础用法（不使用配置框架）-利用IOptionsSnapshot服务读取具名Options")]
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
            serviceCollection.Configure<OrderOption>("bar", it => { it.MaxOrderNum = 900; });

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
                PrintOptionOne();
                PrintOptionTwo();
            }

            public void PrintOptionOne()
            {
                Console.WriteLine($"_option1({_option1.GetHashCode()}):{_option1.AsFormatJsonStr()}");
                Console.WriteLine($"_option2({_option2.GetHashCode()}):{_option2.AsFormatJsonStr()}");
                Console.WriteLine($"_optionsSnapshot1({_optionsSnapshot1.GetHashCode()}):{_optionsSnapshot1.AsFormatJsonStr()}");
                Console.WriteLine($"_optionsSnapshot2({_optionsSnapshot2.GetHashCode()}):{_optionsSnapshot2.AsFormatJsonStr()}");

                PrintOptionCatch(_option1, _optionsSnapshot1);
            }

            public void PrintOptionTwo()
            {
                Console.WriteLine($"_optionsSnapshot1.foo:{_optionsSnapshot1.Get("foo").AsFormatJsonStr()}");

                PrintOptionCatch(_option1, _optionsSnapshot1);
            }

            /// <summary>
            /// 打印内部封装的缓存池
            /// </summary>
            /// <param name="option1"></param>
            /// <param name="_optionsSnapshot1"></param>
            private void PrintOptionCatch(IOptions<OrderOption> option1, IOptionsSnapshot<OrderOption> _optionsSnapshot1)
            {
                var catch1 = option1.GetFieldValue("_cache").GetFieldValue("_cache");
                Console.WriteLine($"option1缓存（{catch1.GetHashCode()}）：{catch1.AsFormatJsonStr()}");
                var catch2 = _optionsSnapshot1.GetFieldValue("_cache").GetFieldValue("_cache");
                Console.WriteLine($"option2缓存（{catch2.GetHashCode()}）：{catch2.AsFormatJsonStr()}");
            }
        }
    }
}
