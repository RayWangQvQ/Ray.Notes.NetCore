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
    [Description("基础用法（不使用配置框架）-利用IOptionsSnapshot服务读取具名Options")]
    public class Test03 : TestBase
    {
        public override void InitConfiguration()
        {
            //不使用配置
        }

        public override void InitServiceProvider()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.Configure<ProfileOption>("foo", it =>
             {
                 it.Gender = Gender.Male;
                 it.Age = 18;
                 it.ContactInfo = new ContactInfo
                 {
                     PhoneNo = "123456789",
                     EmailAddress = "foobar@outlook.com"
                 };
             });
            serviceCollection.Configure<ProfileOption>("bar", it =>
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
            IOptionsSnapshot<ProfileOption> options = Program.ServiceProvider
                .GetRequiredService<IOptionsSnapshot<ProfileOption>>();

            var foo = options.Get("foo");
            Console.WriteLine(JsonSerializer.Serialize(foo).AsFormatJsonStr());

            var bar = options.Get("bar");
            Console.WriteLine(JsonSerializer.Serialize(bar).AsFormatJsonStr());
        }
    }
}
