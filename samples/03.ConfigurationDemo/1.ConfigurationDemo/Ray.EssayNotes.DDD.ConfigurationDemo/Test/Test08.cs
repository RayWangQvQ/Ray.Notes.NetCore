using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Ray.Infrastructure.Extensions;

namespace Ray.EssayNotes.DDD.ConfigurationDemo.Test
{
    [Description("配置文件的变更刷新")]
    public class Test08 : ITest
    {
        public void Run()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile(path: "testsetting.json", optional: true, reloadOnChange: true)
                .Build();

            FormatOptions Bind(IConfiguration configuration)
            {
                return configuration.GetSection("format")
                    .Get<FormatOptions>();
            }

            FormatOptions options = Bind(config);

            ChangeToken.OnChange(() => config.GetReloadToken(), () =>
            {
                Console.WriteLine("触发");
            });

            Console.WriteLine(JsonSerializer.Serialize(options).AsFormatJsonString());

            /** reloadOnChange变更刷新
             * 注意，测试时是修改编译后的文件（比如Debug时是修改bin目录下的文件）
             */
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
