using System;
using System.ComponentModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Ray.EssayNotes.DDD.OptionsDemo.Test
{
    [Description("验证有效性1：valida")]
    public class Test10 : TestBase
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
            services.AddOptions<OrderOption>()
                .Configure(o =>
                {
                    Program.ConfigurationRoot.Bind(o);
                })
                .Validate(o => Validate(o), "最大订单数，必须大于0。");
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

        private bool Validate(OrderOption option)
        {
            return option.MaxOrderNum > 0;
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
