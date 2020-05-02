using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using Ray.Infrastructure.Helpers;

namespace Ray.EssayNotes.DDD.LogSimpleDemo.Test
{
    [Description("过滤日志：利用过滤器根据构建器过滤")]
    public class Test08 : TestBase
    {
        public override void SetLogger()
        {
            Func<string, string, LogLevel, bool> filter = (provider, category, level) =>
            {
                if (provider == typeof(ConsoleLoggerProvider).FullName)
                {
                    return level >= LogLevel.Debug;
                }
                else if (provider == typeof(DebugLoggerProvider).FullName)
                {
                    return level >= LogLevel.Warning;
                }
                else
                {
                    return true;
                }
            };

            _logger = LoggerFactory.Create(builder =>
            {
                builder.AddFilter(filter);//利用构建器添加针对构建器的过滤器
                builder.AddConsole();
                builder.AddDebug();
            })
                .CreateLogger<Test08>();
        }
    }
}
