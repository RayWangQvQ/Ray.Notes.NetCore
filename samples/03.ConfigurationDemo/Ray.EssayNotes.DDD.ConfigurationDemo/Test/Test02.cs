using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using System.Text.Json;
using Ray.Infrastructure.Extensions;
using System.ComponentModel;

namespace Ray.EssayNotes.DDD.ConfigurationDemo.Test
{
    [Description("Section层级")]
    public class Test02 : ITest
    {
        public void Run()
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

            var configuration = new ConfigurationBuilder()
                .Add(new MemoryConfigurationSource { InitialData = source })
                .Build();

            IConfigurationSection section = configuration.GetSection("Format");
            Console.WriteLine(JsonSerializer.Serialize(section).AsFormatJsonString());

            var options = new FormatOptions(section);
            Console.WriteLine(JsonSerializer.Serialize(options).AsFormatJsonString());
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

        public class CurrencyDecimalFormatOptions
        {
            public int Digits { get; set; }
            public string Symbol { get; set; }

            public CurrencyDecimalFormatOptions(IConfiguration config)
            {
                Digits = int.Parse(config["Digits"]);
                Symbol = config["Symbol"];
            }
        }

        public class FormatOptions
        {
            public DateTimeFormatOptions DateTime { get; set; }
            public CurrencyDecimalFormatOptions CurrencyDecimal { get; set; }

            public FormatOptions(IConfiguration config)
            {
                DateTime = new DateTimeFormatOptions(config.GetSection("DateTime"));
                CurrencyDecimal = new CurrencyDecimalFormatOptions(config.GetSection("CurrencyDecimal"));
            }
        }
    }


}
