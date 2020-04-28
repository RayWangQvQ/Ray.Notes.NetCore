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
    [Description("数据源变更刷新-具名的Options")]
    public class Test07 : TestBase
    {
        public override void InitConfiguration()
        {
            Program.ConfigurationRoot = new ConfigurationBuilder()
                .AddJsonFile("profiles.json", true, true)
                .Build();
        }

        public override void InitServiceProvider()
        {
            Program.ServiceProvider = new ServiceCollection()
                .Configure<ProfileOption>("foo", Program.ConfigurationRoot.GetSection("foo"))
                .Configure<ProfileOption>("bar", Program.ConfigurationRoot.GetSection("bar"))
                .BuildServiceProvider();
        }

        public override void Print()
        {
            IOptionsMonitor<ProfileOption> options = Program.ServiceProvider.GetRequiredService<IOptionsMonitor<ProfileOption>>();

            options.OnChange((profile, name) =>
            {
                Console.WriteLine("发生配置变更");//这里并不是只监听到变更的，而是都会进来，即foo进一次bar进一次
                Console.WriteLine(name + profile.AsFormatJsonStr());
            });

            ProfileOption foo = options.Get("foo");
            Console.WriteLine(foo.AsFormatJsonStr());

            ProfileOption bar = options.Get("bar");
            Console.WriteLine(bar.AsFormatJsonStr());
        }
    }
}
