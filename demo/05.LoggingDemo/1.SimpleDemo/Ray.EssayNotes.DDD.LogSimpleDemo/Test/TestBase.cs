using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using Ray.Infrastructure.Extensions.Json;
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

        public virtual void PrintServiceDescriptionsPool()
        {
            if (Program.ServiceProviderRoot != null)
            {
                var josn = Program.ServiceProviderRoot.GetServiceDescriptorsFromScope()
                    .AsJsonStr(option =>
                    {
                        option.SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings
                        {
                            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                        };
                        option.EnumToString = true;
                        option.FilterProps = new FilterPropsOption
                        {
                            FilterEnum = FilterEnum.Ignore,
                            Props = new[] { "Action", "Method" }//属性内容非常长，忽略掉了
                        };
                    }).AsFormatJsonStr();
                Console.WriteLine($"容器中的服务描述池：{josn}");
            }
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
            PrintServiceDescriptionsPool();
            SetLogger();
            PrintLog();
            PrintSomethingOther();
        }
    }
}
