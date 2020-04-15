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
    [Description("基础用法")]
    public class Test01 : TestBase
    {
        public override void InitConfiguration()
        {
            //不使用配置
        }

        public override void InitServiceProvider()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.Configure<Profile>(it =>
            {
                it.Gender = Gender.Male;
                it.Age = 18;
                it.ContactInfo = new ContactInfo
                {
                    PhoneNo = "123456789",
                    EmailAddress = "foobar@outlook.com"
                };
            });

            Program.ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public override void Print()
        {
            IOptions<Profile> options = Program.ServiceProvider
                .GetRequiredService<IOptions<Profile>>();

            var profile = options.Value;
            Console.WriteLine(JsonSerializer.Serialize(profile).AsFormatJsonString());
        }
    }
}
