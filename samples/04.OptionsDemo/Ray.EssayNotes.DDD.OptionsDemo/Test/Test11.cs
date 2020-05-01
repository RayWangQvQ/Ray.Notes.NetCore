using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Ray.EssayNotes.DDD.OptionsDemo.Test
{
    [Description("验证有效性2：DataAnnotations特性验证")]
    public class Test11 : TestBase
    {
        protected override void InitConfiguration()
        {
            Program.ConfigurationRoot = new ConfigurationBuilder()
                .AddJsonFile("order.json", true, true)
                .Build();
        }

        protected override void InitServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddScoped<IOrderService, OrderService>();
            services.AddOptions<NewOrderOption>()
                .Configure(o =>
                {
                    Program.ConfigurationRoot.Bind(o);
                })
                .ValidateDataAnnotations();
            Program.ServiceProvider = services.BuildServiceProvider();
        }

        protected override void Print()
        {
            using (var childScope = Program.ServiceProvider.CreateScope())
            {
                var service = childScope.ServiceProvider.GetRequiredService<IOrderService>();

                service.PrintOption();
            }
        }

        public class NewOrderOption
        {
            [Range(1, 9999)]//添加特性标签
            public int MaxOrderNum { get; set; }
        }

        public class OrderService : IOrderService
        {
            private readonly IOptions<NewOrderOption> _option;

            public OrderService(IOptions<NewOrderOption> option)
            {
                this._option = option;
            }

            public void PrintOption()
            {
                try
                {
                    Console.WriteLine(_option.AsFormatJsonStr());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
