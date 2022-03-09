using System;
using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Ray.EssayNotes.DDD.OptionsDemo.Test
{
    [Description("PostConfigure设置读取之后的附加操作")]
    public class Test09 : TestBase
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
                .Configure(o => o.MaxOrderNum = 100)
                .PostConfigure(o => o.MaxOrderNum++); //读取之后的附加操作
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

        public class OrderService : IOrderService
        {
            private readonly IOptions<OrderOption> _option;

            public OrderService(IOptions<OrderOption> option)
            {
                this._option = option;
            }

            public void PrintOption()
            {
                Console.WriteLine(_option.AsFormatJsonStr());
            }
        }
    }
}
