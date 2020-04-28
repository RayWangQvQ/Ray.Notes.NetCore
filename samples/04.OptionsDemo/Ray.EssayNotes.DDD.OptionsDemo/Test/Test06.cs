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
    [Description("数据源变更刷新-非具名Options")]
    public class Test06 : TestBase
    {
        public override void InitConfiguration()
        {
            Program.ConfigurationRoot = new ConfigurationBuilder()
                .AddJsonFile("profile.json", true, true)//开启配置数据源变更通知
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
            IOptionsMonitor<ProfileOption> options = Program.ServiceProvider.GetRequiredService<IOptionsMonitor<ProfileOption>>();

            options.OnChange(profile =>
            {
                Console.WriteLine("配置变更");
                Console.WriteLine(profile.AsFormatJsonStr());
            });

            var result = options.CurrentValue;
            Console.WriteLine(result.AsFormatJsonStr());
        }
    }
}
