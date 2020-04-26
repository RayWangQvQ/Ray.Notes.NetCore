using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;

namespace Ray.EssayNotes.DDD.ConfigurationDemo.Test
{
    [Description("绑定到实例对象")]
    public class Test05 : ITest
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
            Print1();

            Print2();
        }

        private void Print1()
        {
            var option = new FormatOptions
            {
                CurrencyDecimal = new CurrencyDecimalFormatOptions
                {
                    Digits = 110
                }
            };
            MyConfiguration.Root
                .GetSection("format")
                .Bind(option);

            Console.WriteLine(JsonSerializer.Serialize(option).AsFormatJsonStr());
        }

        /// <summary>
        /// 绑定私有属性
        /// </summary>
        private void Print2()
        {
            var option = new FormatOptions
            {
                CurrencyDecimal = new CurrencyDecimalFormatOptions
                {
                    Digits = 110
                }
            };
            MyConfiguration.Root
                .GetSection("format")
                .Bind(option, binderOptions => binderOptions.BindNonPublicProperties = true);//设置为绑定私有属性

            Console.WriteLine(JsonSerializer.Serialize(option).AsFormatJsonStr());
        }


        public class DateTimeFormatOptions
        {
            public string LongDatePattern { get; set; }
            public string LongTimePattern { get; set; }
            public string ShortDatePattern { get; set; }
            public string ShortTimePattern { get; private set; }//将set设置为私有
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
