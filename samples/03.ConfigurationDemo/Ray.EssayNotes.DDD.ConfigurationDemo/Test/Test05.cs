using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Ray.Infrastructure.Extensions;

namespace Ray.EssayNotes.DDD.ConfigurationDemo.Test
{
    [Description("多份配置文件作为绑定数据源")]
    public class Test05 : ITest
    {
        public void Run()
        {
            var dic = new Dictionary<string, string>
            {
                {"预发环境", "staging"},
                {"产品环境", "production"},
            };
            Console.WriteLine($"请输入环境：{JsonSerializer.Serialize(dic).AsFormatJsonString()}");
            string env = Console.ReadLine();

            FormatOptions options = new ConfigurationBuilder()
                .AddJsonFile("testsetting.json")
                .AddJsonFile($"testsetting.{env}.json", true)
                .Build()
                .GetSection("format")
                .Get<FormatOptions>();
            /** 
             * 原理：绑定配置文件源和向容器注册是类似的，是覆盖的
             */

            Console.WriteLine(JsonSerializer.Serialize(options).AsFormatJsonString());
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
