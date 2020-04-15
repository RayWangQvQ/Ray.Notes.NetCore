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
    [Description("变更刷新")]
    public class Test03 : Test02
    {
        public override void InitConfiguration()
        {
            Program.ConfigurationRoot = new ConfigurationBuilder()
                .AddJsonFile("profile.json", true, true)//开启配置数据源变更通知
                .Build();
        }

        public override void Print()
        {
            var options = Program.ServiceProvider
                .GetRequiredService<IOptionsMonitor<Profile>>();

            options.OnChange(profile =>
            {
                Console.WriteLine("配置变更");
                Console.WriteLine(JsonSerializer.Serialize(profile).AsFormatJsonString());
            });

            var result = options.CurrentValue;
            Console.WriteLine(JsonSerializer.Serialize(result).AsFormatJsonString());
        }
    }
}
