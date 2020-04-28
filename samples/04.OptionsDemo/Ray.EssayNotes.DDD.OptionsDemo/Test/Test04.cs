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
    [Description("结合配置系统")]
    public class Test04 : TestBase
    {
        public override void InitConfiguration()
        {
            Program.ConfigurationRoot = new ConfigurationBuilder()
                .AddJsonFile("profile.json")
                .Build();
        }

        public override void InitServiceProvider()
        {
            Program.ServiceProvider = new ServiceCollection()
                .AddOptions()
                .Configure<ProfileOption>(Program.ConfigurationRoot)
                .BuildServiceProvider();
        }

        public override void Print()
        {
            IOptions<ProfileOption> options = Program.ServiceProvider.GetRequiredService<IOptions<ProfileOption>>();

            var profile = options.Value;
            Console.WriteLine(profile.AsFormatJsonStr());
        }
    }
}
