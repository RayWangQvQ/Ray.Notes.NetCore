using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using Ray.Infrastructure.Helpers;

namespace Ray.EssayNotes.DDD.LogSimpleDemo.Test
{
    [Description("基础用法")]
    public class Test01 : TestBase
    {
        public override void SetLogger()
        {
            var factory = LoggerFactory.Create(builder =>
            {
                /**
                 * 控制台输出日志
                 * 需要导入Microsoft.Extensions.Logging.Console包
                 * 会注册ConsoleLoggerProvider
                 */
                builder.AddConsole();

                /**
                 * 调试器输出日志(vs的"输出"窗口查看)
                 * 需要导入Microsoft.Extensions.Logging.Debug包
                 * 会注册DebugLoggerProvider
                 */
                builder.AddDebug();
            });
            this._logger = factory.CreateLogger("Test01Logger");
        }

        /// <summary>
        /// 打印logger对象
        /// </summary>
        public override void PrintSomethingOther()
        {
            Console.WriteLine(_logger.AsJsonStr(option =>
            {
                option.SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                };
                option.EnumToString = true;
            }).AsFormatJsonStr());
        }
    }
}
