using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ray.Infrastructure.Helpers;

namespace Ray.EssayNotes.DDD.LogSimpleDemo.Test
{
    [Description("TraceSource和EventSource")]
    public class Test03 : TestBase
    {
        public override void SetLogger()
        {
            var listener = new FoobarEventListener();
            listener.EventSourceCreated += (sender, args) =>
            {
                if (args.EventSource.Name == "Microsoft-Extensions-Logging")
                {
                    listener.EnableEvents(args.EventSource, EventLevel.LogAlways);
                }
            };
            listener.EventWritten += (sender, args) =>
            {
                if (args.EventName == "FormattedMessage")
                {
                    var payload = args.Payload;
                    var payloadNames = args.PayloadNames;
                    var indexOfLevel = payloadNames.IndexOf("Level");
                    var indexOfCategory = args.PayloadNames.IndexOf("LoggerName");
                    var indexOfEventId = args.PayloadNames.IndexOf("EventId");
                    var indexOfMessage = args.PayloadNames.IndexOf("FormattedMessage");
                    Console.WriteLine($"{payload[indexOfLevel],-11}:{payload[indexOfCategory]}[{payload[indexOfEventId]}]");
                    Console.WriteLine($"{"",-13}{payload[indexOfMessage]}");
                }
            };

            _logger = LoggerFactory.Create(builder =>
            {
                /**
                 * Trace文件输出日志
                 * 需要导入Microsoft.Extensions.Logging.TraceSource包
                 * 会注册TraceSourceLoggerProvider
                 */
                builder.AddTraceSource(new SourceSwitch("default", "All"), new DefaultTraceListener { LogFileName = "trace.log" });

                /**
                 * 事件输出日志
                 * 需要导入Microsoft.Extensions.Logging.EventSource包
                 * EventSourceLoggerProvider
                 */
                builder.AddEventSourceLogger();
            })
                .CreateLogger<Program>();
        }

        public class FoobarEventListener : EventListener { }
    }
}
