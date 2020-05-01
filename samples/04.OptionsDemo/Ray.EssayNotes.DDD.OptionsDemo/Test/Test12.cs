using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Ray.EssayNotes.DDD.OptionsDemo.Test
{
    [Description("验证有效性3：实现接口验证")]
    public class Test12 : TestBase
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
                });
            services.AddSingleton<IValidateOptions<NewOrderOption>, NewOrderOption>();
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

        public class NewOrderOption : IValidateOptions<NewOrderOption>
        {
            public int MaxOrderNum { get; set; }

            public ValidateOptionsResult Validate(string name, NewOrderOption options)
            {
                if (options.MaxOrderNum < 1) return ValidateOptionsResult.Fail("最大订单数，必须大于0");
                else return ValidateOptionsResult.Success;
            }
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
