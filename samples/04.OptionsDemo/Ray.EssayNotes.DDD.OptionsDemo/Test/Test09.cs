using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Ray.EssayNotes.DDD.OptionsDemo.Test
{
    [Description("验证有效性")]
    public class Test09 : TestBase
    {
        protected override void InitConfiguration()
        {
            //
        }

        protected override void InitServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddOptions<ProfileOption>()
                //.Configure(o => o.Age = 9999)
                .Configure(o => o.Age = 22)
                .Validate(o => Validate(o), "年龄异常，建国以后不允许成精。");
            Program.ServiceProvider = services.BuildServiceProvider();
        }

        protected override void Print()
        {
            try
            {
                var option = Program.ServiceProvider.GetRequiredService<IOptions<ProfileOption>>();
                Console.WriteLine(option.Value);
            }
            catch (OptionsValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private bool Validate(ProfileOption option)
        {
            return option.Age > 0 && option.Age < 200;
        }
    }
}
