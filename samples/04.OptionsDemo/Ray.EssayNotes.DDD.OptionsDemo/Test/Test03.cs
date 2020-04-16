using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ray.Infrastructure.Extensions;

namespace Ray.EssayNotes.DDD.OptionsDemo.Test
{
    [Description("结合配置系统")]
    public class Test03 : TestBase
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
                .AddOptions()//注册options服务，使可以使用IOptions<TOptions>、IOptionsMonitor<TOptions>以及IOptionsSnapshot<TOptions>等功能
                .Configure<Profile>(Program.ConfigurationRoot)
                .BuildServiceProvider();
        }

        public override void Print()
        {
            IOptions<Profile> options = Program.ServiceProvider
                .GetRequiredService<IOptions<Profile>>();

            var profile = options.Value;
            Console.WriteLine(JsonSerializer.Serialize(profile).AsFormatJsonStr());
        }
    }
}
