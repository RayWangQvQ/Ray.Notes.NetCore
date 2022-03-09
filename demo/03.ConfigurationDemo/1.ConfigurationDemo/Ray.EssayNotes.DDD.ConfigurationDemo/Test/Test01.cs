using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using System.Text.Json;
using System.ComponentModel;
using System.Text.Json;

namespace Ray.EssayNotes.DDD.ConfigurationDemo.Test
{
    [Description("基础用法01：创建、绑定数据源、使用")]
    public class Test01 : ITest
    {
        public void Init()
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
            IConfigurationSource configurationSource = new MemoryConfigurationSource { InitialData = source };
            builder.Add(configurationSource);

            //3.构建IConfigurationRoot
            MyConfiguration.Root = builder.Build();
        }

        public void Run()
        {
            Console.WriteLine($"key1：{MyConfiguration.Root["key1"]}");
            Console.WriteLine($"key2：{MyConfiguration.Root["key2"]}");

            Console.WriteLine($"section1:key3：{MyConfiguration.Root["section1:key3"]}");

            //如果路径不全（不是叶子节点），则为null
            string s = MyConfiguration.Root["section2:section3"];
            Console.WriteLine($"section2:section3：{s}");

            //key不存在，返回null
            var s2 = MyConfiguration.Root["123"];
            Console.WriteLine($"123：{s2}");
        }
    }


}
