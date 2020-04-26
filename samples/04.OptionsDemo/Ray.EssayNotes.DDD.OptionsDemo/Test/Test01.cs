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
            if (Program.ServiceProvider != null) return;

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
            //从根容器中获取
            Print(Program.ServiceProvider);
            this.PrintResolvedServices(Program.ServiceProvider, "根容器");

            //从子容器中获取
            using (var childScope = Program.ServiceProvider.CreateScope())
            {
                Print(childScope.ServiceProvider);
                this.PrintResolvedServices(childScope.ServiceProvider, "子容器");
            }
        }

        private void Print(IServiceProvider serviceProvider)
        {
            //从容器中获取
            IOptions<ProfileOption> option1 = serviceProvider
                .GetRequiredService<IOptions<ProfileOption>>();
            var option2 = serviceProvider
                .GetRequiredService<IOptionsSnapshot<ProfileOption>>();

            //打印值
            ProfileOption profile1 = option1.Value;
            Console.WriteLine(JsonSerializer.Serialize(profile1).AsFormatJsonStr());
            ProfileOption profile2 = option2.Value;
            Console.WriteLine(JsonSerializer.Serialize(profile2).AsFormatJsonStr());
        }
    }
}
