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
    [Description("具名的Options")]
    public class Test04 : TestBase
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
                .AddOptions()
                .Configure<Profile>("foo", Program.ConfigurationRoot.GetSection("foo"))
                .Configure<Profile>("bar", Program.ConfigurationRoot.GetSection("bar"))
                .BuildServiceProvider();
        }

        public override void Print()
        {
            IOptionsSnapshot<Profile> options = Program.ServiceProvider.GetRequiredService<IOptionsSnapshot<Profile>>();

            Profile foo = options.Get("foo");
            Console.WriteLine(JsonSerializer.Serialize(foo).AsFormatJsonString());

            Profile bar = options.Get("bar");
            Console.WriteLine(JsonSerializer.Serialize(bar).AsFormatJsonString());
        }
    }
}
