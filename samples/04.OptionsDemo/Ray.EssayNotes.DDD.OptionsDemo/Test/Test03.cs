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
    [Description("IOptions与IOptionsSnapshot")]
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

                PrintOptionCatch(_option1, _optionsSnapshot1);
            }

            /// <summary>
            /// 打印内部封装的缓存
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
