using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Ray.EssayNotes.DDD.LogSimpleDemo.Test
{
    [Description("结合DI容器-解析ILogger对象")]
    public class Test05 : TestBase
    {
        public override void InitContanier()
        {
            var services = new ServiceCollection();
            services.AddLogging(builder =>
            {
                builder.AddConsole()
                .AddDebug()
                .AddTraceSource(new SourceSwitch("default", "All"))
                .AddEventSourceLogger();
            });
            Program.ServiceProviderRoot = services.BuildServiceProvider();
        }

        public override void SetLogger()
        {
            _logger = Program.ServiceProviderRoot.GetService<ILogger>();//从容器中直接拿ILogger
        }
    }
}
