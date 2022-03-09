using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using Ray.Infrastructure.Helpers;

namespace Ray.EssayNotes.DDD.LogSimpleDemo.Test
{
    [Description("过滤日志：根据日志等级")]
    public class Test06 : TestBase
    {
        public override void SetLogger()
        {
            _logger = LoggerFactory.Create(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Trace);//日志等级默认为Information，可以利用构建器手动设置
                builder.AddConsole();
                builder.AddDebug();
            })
                .CreateLogger<Test06>();
        }
    }
}
