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
    [Description("具名的Options的数据源变更刷新")]
    public class Test05 : Test04
    {
        public override void InitConfiguration()
        {
            Program.ConfigurationRoot = new ConfigurationBuilder()
                .AddJsonFile("profiles.json", true, true)
                .Build();
        }

        public override void Print()
        {
            IOptionsMonitor<Profile> options = Program.ServiceProvider
                .GetRequiredService<IOptionsMonitor<Profile>>();

            options.OnChange((profile, name) =>
            {
                Console.WriteLine("发生配置变更");//这里并不是只监听到变更的，而是都会进来，即foo进一次bar进一次
                Console.WriteLine(name + JsonSerializer.Serialize(profile).AsFormatJsonString());
            });

            Profile foo = options.Get("foo");
            Console.WriteLine(JsonSerializer.Serialize(foo).AsFormatJsonString());

            Profile bar = options.Get("bar");
            Console.WriteLine(JsonSerializer.Serialize(bar).AsFormatJsonString());
        }
    }
}
