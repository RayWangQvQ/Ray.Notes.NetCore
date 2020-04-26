using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using System.Text.Json;
using System.ComponentModel;

namespace Ray.EssayNotes.DDD.ConfigurationDemo.Test
{
    [Description("基础用法02")]
    public class Test02 : ITest
    {
        public void Init()
        {
            var source = new Dictionary<string, string>
            {
                ["longDatePattern"] = "dddd, MMMM d, yyyy",
                ["longTimePattern"] = "h:mm:ss tt",
                ["shortDatePattern"] = "M/d/yyyy",
                ["shortTimePattern"] = "h:mm tt"
            };

            //1.生成配置的构建器
            var builder = new ConfigurationBuilder();
            //2.利用构建器进行配置（比如绑定数据源等操作）
            builder.Add(new MemoryConfigurationSource { InitialData = source });
            //3.构建IConfigurationRoot
            MyConfiguration.Root = builder.Build();
        }

        public void Run()
        {
            var options = new DateTimeFormatOptions(MyConfiguration.Root);

            Console.WriteLine(JsonSerializer.Serialize(options).AsFormatJsonStr());
        }

        public class DateTimeFormatOptions
        {
            public string LongDatePattern { get; set; }
            public string LongTimePattern { get; set; }
            public string ShortDatePattern { get; set; }
            public string ShortTimePattern { get; set; }

            public DateTimeFormatOptions(IConfiguration config)
            {
                LongDatePattern = config["LongDatePattern"];
                LongTimePattern = config["LongTimePattern"];
                ShortDatePattern = config["ShortDatePattern"];
                ShortTimePattern = config["ShortTimePattern"];
            }
        }
    }


}
