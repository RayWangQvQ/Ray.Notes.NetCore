using System;
using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Ray.EssayNotes.DDD.OptionsDemo.Test
{
    [Description("验证有效性")]
    public class Test10 : TestBase
    {
        protected override void InitConfiguration()
        {
            //
        }

        protected override void InitServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddScoped<IOrderService, OrderService>();
            services.AddOptions<OrderOption>()
                //.Configure(o => o.MaxOrderNum = 110)
                .Configure(o => o.MaxOrderNum = -1)
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
