﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;

namespace Ray.EssayNotes.DDD.ConfigurationDemo.Test
{
    [Description("数据源：将配置文件添加为数据源")]
    public class Test30 : ITest
    {
        public void Init()
        {
            MyConfiguration.Root = new ConfigurationBuilder()
                .AddJsonFile("testsetting.json")
                .Build();
            /** 这里的AddJsonFile()需要导包Microsoft.Extensions.Configuration.Json
             * 用于将数据源绑定到json文件
             * json文件需要设置为始终赋值到输出目录
             */
        }

        public void Run()
        {
            FormatOptions options = MyConfiguration.Root
                .GetSection("format")
                .Get<FormatOptions>();

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
