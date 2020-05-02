using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using Ray.Infrastructure.Helpers;

namespace Ray.EssayNotes.DDD.LogSimpleDemo.Test
{
    [Description("过滤日志：利用过滤器根据类别（category）设置过滤")]
    public class Test07 : TestBase
    {
        private ILogger _fooLogger;
        private ILogger _barLogger;
        private ILogger _bazLogger;

        public override void SetLogger()
        {
            Func<string, LogLevel, bool> filter = (category, level) =>
            {
                switch (category)
                {
                    case "Foo": return level >= LogLevel.Debug;
                    case "Bar": return level >= LogLevel.Warning;
                    case "Baz": return level >= LogLevel.None;
                    default: return level >= LogLevel.Information;
                }
            };

            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddFilter(filter);//利用构建器添加针对日志的类别的过滤器
                builder.AddConsole();
                builder.AddDebug();
            });

            _fooLogger = loggerFactory.CreateLogger("Foo");
            _barLogger = loggerFactory.CreateLogger("Bar");
            _bazLogger = loggerFactory.CreateLogger("Baz");
        }

        public override void PrintLog()
        {
            List<LogLevel> levels = EnumHelper.AsArray<LogLevel>()
               .Where(it => it != LogLevel.None)
               .ToList();

            var eventId = 1;
            levels.ForEach(level => _fooLogger.Log(level, eventId++, "这是一条 {0} 日志信息.", level));

            eventId = 1;
            levels.ForEach(level => _barLogger.Log(level, eventId++, "这是一条 {0} 日志信息.", level));

            eventId = 1;
            levels.ForEach(level => _bazLogger.Log(level, eventId++, "这是一条 {0} 日志信息.", level));
        }
    }
}
