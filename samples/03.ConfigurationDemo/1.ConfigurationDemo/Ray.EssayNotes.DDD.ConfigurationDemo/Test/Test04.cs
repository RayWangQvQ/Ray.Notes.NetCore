using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Ray.Infrastructure.Extensions;

namespace Ray.EssayNotes.DDD.ConfigurationDemo.Test
{
    [Description("直接绑定到POCO对象")]
    public class Test04 : ITest
    {
        public void Init()
        {
            var source = new Dictionary<string, string>
            {
                ["format:dateTime:longDatePattern"] = "dddd, MMMM d, yyyy",
                ["format:dateTime:longTimePattern"] = "h:mm:ss tt",
                ["format:dateTime:shortDatePattern"] = "M/d/yyyy",
                ["format:dateTime:shortTimePattern"] = "h:mm tt",

                ["format:currencyDecimal:digits"] = "2",
                ["format:currencyDecimal:symbol"] = "$",
            };

            MyConfiguration.Root = new ConfigurationBuilder()
                .Add(new MemoryConfigurationSource { InitialData = source })
                .Build();
        }

        public void Run()
        {
            FormatOptions options = MyConfiguration.Root
                .GetSection("format")
                .Get<FormatOptions>();
            /**
             * 这里的Get<T>方法为Microsoft.Extensions.Configuration.ConfigurationBinder.Get<T>，需要导包
             * 其可直接将IConfigurationSection绑定到指定的POCO对象
             * 这里的T必须有无参的构造函数，否则会异常
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
