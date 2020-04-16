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
    [Description("基础用法-具名Options（不使用配置框架）")]
    public class Test02 : TestBase
    {
        public override void InitConfiguration()
        {
            //不使用配置
        }

        public override void InitServiceProvider()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.Configure<Profile>("foo", it =>
             {
                 it.Gender = Gender.Male;
                 it.Age = 18;
                 it.ContactInfo = new ContactInfo
                 {
                     PhoneNo = "123456789",
                     EmailAddress = "foobar@outlook.com"
                 };
             });
            serviceCollection.Configure<Profile>("bar", it =>
            {
                it.Gender = Gender.Female;
                it.Age = 25;
                it.ContactInfo = new ContactInfo
                {
                    PhoneNo = "456",
                    EmailAddress = "bar@outlook.com"
                };
            });

            Program.ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public override void Print()
        {
            IOptionsSnapshot<Profile> options = Program.ServiceProvider
                .GetRequiredService<IOptionsSnapshot<Profile>>();

            var foo = options.Get("foo");
            Console.WriteLine(JsonSerializer.Serialize(foo).AsFormatJsonStr());

            var bar = options.Get("bar");
            Console.WriteLine(JsonSerializer.Serialize(bar).AsFormatJsonStr());
        }
    }
}
