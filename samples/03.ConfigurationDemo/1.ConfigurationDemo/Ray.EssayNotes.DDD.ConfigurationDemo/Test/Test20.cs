using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;

namespace Ray.EssayNotes.DDD.ConfigurationDemo.Test
{
    [Description("映射为POCO：Bind绑定到已存在实例对象")]
    public class Test20 : ITest
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
            Print3();
        }

        private void Print1()
        {
            var option = new FormatOptions();

            MyConfiguration.Root
                .GetSection("format")
                .Bind(option);//Bind函数绑定

            Console.WriteLine(JsonSerializer.Serialize(option).AsFormatJsonStr());

            /*
             * 绑定会为所有public属性赋值
             * 其中因为option.DateTime.ShortTimePattern的set方法为private，所以不会绑定到配置值，为null
             */
        }

        /// <summary>
        /// 测试覆盖
        /// </summary>
        private void Print2()
        {
            var option = new FormatOptions
            {
                DateTime = new DateTimeFormatOptions
                {
                    LongDatePattern = "测试覆盖"
                }
            };

            MyConfiguration.Root
                .GetSection("format")
                .Bind(option);//Bind函数绑定

            Console.WriteLine(JsonSerializer.Serialize(option).AsFormatJsonStr());

            /*
             * 本身已有值的会被绑定覆盖，
             * 即option.DateTime.LongDatePattern的值最后为配置值"dddd, MMMM d, yyyy"
             */
        }

        /// <summary>
        /// 测试绑定私有属性
        /// </summary>
        private void Print3()
        {
            var option = new FormatOptions();
            MyConfiguration.Root
                .GetSection("format")
                .Bind(option, binderOptions => binderOptions.BindNonPublicProperties = true);//设置绑定私有属性

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
