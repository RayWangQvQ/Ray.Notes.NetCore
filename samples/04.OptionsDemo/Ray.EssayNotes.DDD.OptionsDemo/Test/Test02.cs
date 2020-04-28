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
    [Description("IOptions与IOptionsSnapshot")]
    public class Test02 : TestBase
    {
        public override void InitConfiguration()
        {
            //不使用配置系统
        }

        public override void InitServiceProvider()
        {
            if (Program.ServiceProvider != null) return;

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddOptions();
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
            Console.WriteLine("从根容器中获取：");
            Print(Program.ServiceProvider);
            this.PrintResolvedServices(Program.ServiceProvider, "根容器");

            Console.WriteLine("从子容器中获取：");
            using (var childScope = Program.ServiceProvider.CreateScope())
            {
                Print(childScope.ServiceProvider);
                this.PrintResolvedServices(childScope.ServiceProvider, "子容器");
            }
        }

        private void Print(IServiceProvider serviceProvider)
        {
            //从容器中获取
            IOptions<ProfileOption> option1 = serviceProvider.GetRequiredService<IOptions<ProfileOption>>();
            IOptionsSnapshot<ProfileOption> option2 = serviceProvider.GetRequiredService<IOptionsSnapshot<ProfileOption>>();

            //打印option
            Console.WriteLine($"option1：{option1.AsFormatJsonStr()}");
            Console.WriteLine($"option2：{option2.AsFormatJsonStr()}");

            //打印缓存
            Console.WriteLine($"option1缓存：{option1.GetFieldValue("_cache").GetFieldValue("_cache").AsFormatJsonStr()}");
            Console.WriteLine($"option2缓存：{option2.GetFieldValue("_cache").GetFieldValue("_cache").AsFormatJsonStr()}");
        }
    }
}
