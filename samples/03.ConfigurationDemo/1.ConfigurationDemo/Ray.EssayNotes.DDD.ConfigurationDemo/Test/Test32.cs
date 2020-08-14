using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Ray.EssayNotes.DDD.ConfigurationDemo.Test
{
    [Description("配置文件的optional")]
    public class Test32 : ITest
    {
        public void Init()
        {
            var dic = new Dictionary<string, string>
            {
                {"预发环境", "staging"},
                {"产品环境", "production"},
            };
            Console.WriteLine($"请输入环境：{JsonSerializer.Serialize(dic).AsFormatJsonStr()}");
            string env = Console.ReadLine();

            MyConfiguration.Root = new ConfigurationBuilder()
                .AddJsonFile("testsetting.json", false)
                .AddJsonFile($"testsetting.{env}.json", false)
                .Build();
        }

        public void Run()
        {
            FormatOptions options = MyConfiguration.Root
                .GetSection("format")
                .Get<FormatOptions>();
            /** optional是否可选，默认为true
             * 即如果是true，表示配置文件是可有可无的，绑定的时候找不到对应的文件不会异常
             * 如果是false，表示该配置文件是必须的，绑定的时候系统找不到对应文件就直接报异常了
             * 这里设为true，如果输入的环境字符串不存在对应文件，则直接报异常
             */

            Console.WriteLine(JsonSerializer.Serialize(options).AsFormatJsonStr());
        }


        public class DateTimeFormatOptions
        {
            public string LongDatePattern { get; set; }
            public string LongTimePattern { get; set; }
            public string ShortDatePattern { get; set; }
            public string ShortTimePattern { get; set; }
        }

        public class CurrencyDecimalFormatOptions
        {
            public int Digits { get; set; }
            public string Symbol { get; set; }
        }

        public class FormatOptions
        {
            public DateTimeFormatOptions DateTime { get; set; }
            public CurrencyDecimalFormatOptions CurrencyDecimal { get; set; }
        }
    }
}
