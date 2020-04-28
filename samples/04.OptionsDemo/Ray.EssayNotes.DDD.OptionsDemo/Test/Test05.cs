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
    [Description("具名的Options")]
    public class Test05 : TestBase
    {
        public override void InitConfiguration()
        {
            Program.ConfigurationRoot = new ConfigurationBuilder()
                .AddJsonFile("profiles.json")
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
            IOptionsSnapshot<ProfileOption> options = Program.ServiceProvider.GetRequiredService<IOptionsSnapshot<ProfileOption>>();

            ProfileOption foo = options.Get("foo");
            Console.WriteLine(foo.AsFormatJsonStr());

            ProfileOption bar = options.Get("bar");
            Console.WriteLine(bar.AsFormatJsonStr());
        }
    }
}
