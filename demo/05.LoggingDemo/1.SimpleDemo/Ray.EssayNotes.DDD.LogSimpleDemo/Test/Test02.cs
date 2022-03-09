using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using Ray.Infrastructure.Helpers;

namespace Ray.EssayNotes.DDD.LogSimpleDemo.Test
{
    [Description("基础用法：创建ILogger时使用泛型指定日志类别")]
    public class Test02 : TestBase
    {
        public override void SetLogger()
        {
            _logger = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            })
                .CreateLogger<Test02>();//传入泛型
        }
    }
}
