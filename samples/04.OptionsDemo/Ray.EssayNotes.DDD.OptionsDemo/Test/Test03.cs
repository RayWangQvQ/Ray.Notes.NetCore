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
    [Description("结合配置系统")]
    public class Test03 : TestBase
    {
        public override void InitConfiguration()
        {
            Program.ConfigurationRoot = new ConfigurationBuilder()
                .AddJsonFile("profile.json")
                .Build();
        }

        public override void InitServiceProvider()
        {
            Program.ServiceProvider = new ServiceCollection()
                //.AddOptions()
                .Configure<ProfileOption>(Program.ConfigurationRoot)
                .BuildServiceProvider();

            /**
             * 该方法将Options模型中的几个核心类型作为服务注册到了指定的IServiceCollection对象之中，使可以使用IOptions<TOptions>、IOptionsMonitor<TOptions>以及IOptionsSnapshot<TOptions>等功能
             * 由于它们都是调用TryAdd方法进行服务注册的，所以我们可以在需要Options模式支持的情况下调用AddOptions方法，而不需要担心是否会添加太多重复服务注册的问题。
             * 比如下面的Configure方法内部也会调用，所以这里可以省略这句
             */
        }

        public override void Print()
        {
            IOptions<ProfileOption> options = Program.ServiceProvider
                .GetRequiredService<IOptions<ProfileOption>>();

            var profile = options.Value;
            Console.WriteLine(JsonSerializer.Serialize(profile).AsFormatJsonStr());
        }
    }
}
