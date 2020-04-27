using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Ray.Infrastructure.Extensions.Json;

namespace Ray.EssayNotes.DDD.OptionsDemo.Test
{
    public abstract class TestBase
    {
        /// <summary>
        /// 初始化配置树
        /// </summary>
        public abstract void InitConfiguration();

        /// <summary>
        /// 初始化依赖注入容器
        /// </summary>
        public abstract void InitServiceProvider();

        /// <summary>
        /// 打印容器的服务描述池
        /// </summary>
        private void PrintServiceDescriptors()
        {
            Console.WriteLine($"\r\n容器中的服务描述池：");
            List<ServiceDescriptor> serviceDescriptors = Program.ServiceProvider?.GetServiceDescriptorsFromScope()
                .ToList();

            string re = serviceDescriptors.AsJsonStr(
                option =>
                {
                    option.EnumToString = true;
                    option.IgnoreProps = new IgnoreOption
                    {
                        LimitPropsEnum = LimitPropsEnum.Ignore,
                        Props = new[] { "Action" }//Action属性内容非常长，忽略掉了
                    };
                },
                setting =>
                {
                    setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    setting.MaxDepth = 1;
                }).AsFormatJsonStr();

            Console.WriteLine(re);
            File.WriteAllText("./test.txt", re);
        }

        /// <summary>
        /// 打印测试结果
        /// </summary>
        public abstract void Print();

        public virtual void Run()
        {
            InitConfiguration();
            InitServiceProvider();
            PrintServiceDescriptors();
            Print();
        }

        /// <summary>
        /// 打印容器的持久化实例池
        /// </summary>
        protected void PrintResolvedServices(IServiceProvider serviceProvider, string name = "")
        {
            Console.WriteLine($"\r\n{name}容器中的持久化实例池：");
            var dic = serviceProvider.GetResolvedServicesFromScope()
                .ToDictionary(x => x.Key.Type.FullName, x => x.Value);
            Console.WriteLine(dic.AsFormatJsonStr(false));
        }
    }
}
