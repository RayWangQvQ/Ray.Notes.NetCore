using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using System.Text.Json;
using Ray.Infrastructure.Extensions;
using System.ComponentModel;
using System.Text.Json;

namespace Ray.EssayNotes.DDD.ConfigurationDemo.Test
{
    [Description("基础用法01")]
    public class Test01 : ITest
    {
        public void Run()
        {
            var source = new Dictionary<string, string>
            {
                ["key1"] = "value1",
                ["key2"] = "value2",
                ["section1:key3"] = "value3",
                ["section2:section3:key4"] = "value4"
            };

            //1.生成配置的构建器
            var builder = new ConfigurationBuilder();
            //2.利用构建器进行配置（比如绑定数据源等操作）
            builder.Add(new MemoryConfigurationSource { InitialData = source });
            //3.构建IConfigurationRoot
            IConfigurationRoot config = builder.Build();

            Console.WriteLine(config["key1"]);
            Console.WriteLine(config["section1:key3"]);

            IConfigurationSection section = config.GetSection("section2").GetSection("section3");
            Console.WriteLine(section["key4"]);

            //叶子节点也是特殊的section，是一个有值的section
            var section2 = config.GetSection("key2");
            Console.WriteLine(section2.Value);
            Console.WriteLine(JsonSerializer.Serialize(section2));
        }
    }


}
