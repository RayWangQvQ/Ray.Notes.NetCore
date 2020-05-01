using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using Ray.Infrastructure.Helpers;

namespace Ray.EssayNotes.DDD.LogSimpleDemo.Test
{
    public abstract class TestBase
    {
        protected ILogger _logger;

        /// <summary>
        /// 初始化配置框架
        /// </summary>
        public virtual void InitConfiguration()
        {

        }

        /// <summary>
        /// 初始化DI容器
        /// </summary>
        public virtual void InitContanier()
        {

        }

        /// <summary>
        /// 生成Logger
        /// </summary>
        public abstract void SetLogger();

        /// <summary>
        /// 打印日志
        /// </summary>
        public virtual void PrintLog()
        {
            List<LogLevel> levels = EnumHelper.AsArray<LogLevel>()
               .Where(it => it != LogLevel.None)
               .ToList();
            var eventId = 1;
            levels.ForEach(level => _logger.Log(level, eventId++, "这是一条 {0} 日志信息.", level));
        }

        /// <summary>
        /// 打印点其他信息
        /// </summary>
        public virtual void PrintSomethingOther()
        {

        }

        public virtual void Run()
        {
            InitConfiguration();
            InitContanier();
            SetLogger();
            PrintLog();
            PrintSomethingOther();
        }
    }
}
