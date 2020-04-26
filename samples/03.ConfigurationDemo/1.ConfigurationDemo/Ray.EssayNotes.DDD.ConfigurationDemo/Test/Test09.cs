using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace Ray.EssayNotes.DDD.ConfigurationDemo.Test
{
    [Description("配置文件的变更刷新")]
    public class Test09 : ITest
    {
        public void Init()
        {
            if (MyConfiguration.Root == null)
                MyConfiguration.Root = new ConfigurationBuilder()
                    .AddJsonFile(path: "testsetting.json", optional: true, reloadOnChange: true)
                    .Build();
            /** reloadOnChange变更刷新
             * 注意，测试时是修改编译后的文件（比如Debug时是修改bin目录下的文件）
             */
        }

        public void Run()
        {
            var formatOptions = MyConfiguration.Root.GetSection("format")
                .Get<FormatOptions>();
            Console.WriteLine(JsonSerializer.Serialize(formatOptions).AsFormatJsonStr());

            ChangeToken.OnChange(() => MyConfiguration.Root.GetReloadToken(), () =>
             {
                 Console.WriteLine("触发配置变更");
                 /**
                  * 当对应的配置文件的reloadOnChange为true，当文件发生变更时，会触发
                  * 注意：这里只需要对变更做处理，不需要再重新从文件build，系统会自动更新IConfigurationRoot，即进入这里的时候，IConfigurationRoot已经是同步后的了。
                  * p.s.测试修改配置文件的时候，使用notepad编辑，会出现触发多次的情况，这是notepad的原因，使用默认文本编辑器并不会，很坑
                  */
             });
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
