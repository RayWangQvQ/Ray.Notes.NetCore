using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Ray.EssayNotes.DDD.OptionsDemo.Test
{
    [Description("基础用法（不使用配置框架）-利用IOptions服务读取非具名Options")]
    public class Test01 : TestBase
    {
        public override void InitConfiguration()
        {
            //不使用配置系统
        }

        public override void InitServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.Configure<ProfileOption>(it =>
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
            IOptions<ProfileOption> option = Program.ServiceProvider
                .GetRequiredService<IOptions<ProfileOption>>();

            Console.WriteLine(option.AsFormatJsonStr());
        }
    }
}
