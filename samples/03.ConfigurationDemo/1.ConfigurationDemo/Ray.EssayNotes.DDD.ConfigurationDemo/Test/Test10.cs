using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using System.Text.Json;
using System.ComponentModel;

namespace Ray.EssayNotes.DDD.ConfigurationDemo.Test
{
    [Description("Section节点")]
    public class Test10 : ITest
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

            MyConfiguration.Root = new ConfigurationBuilder()
                .Add(new MemoryConfigurationSource { InitialData = source })
                .Build();
        }

        public void Run()
        {
            IConfigurationSection section1 = MyConfiguration.Root.GetSection("section1");
            Console.WriteLine(JsonSerializer.Serialize(section1));
            /*
             * 返回{"Key":"section1","Path":"section1","Value":null}
             */

            IConfigurationSection section3 = MyConfiguration.Root.GetSection("section2").GetSection("section3");
            Console.WriteLine(JsonSerializer.Serialize(section3));
            /*
             * 返回{"Key":"section3","Path":"section2:section3","Value":null}
             */

            //IConfigurationSection接口继承了IConfiguration，同样可以读取值
            Console.WriteLine(section3["key4"]);


            //叶子节点也是特殊的section，是一个有值的section
            IConfigurationSection section = MyConfiguration.Root.GetSection("key1");
            Console.WriteLine(JsonSerializer.Serialize(section));
            /*
             * 返回{"Key":"key1","Path":"key1","Value":"value1"}
             */
        }
    }
}
