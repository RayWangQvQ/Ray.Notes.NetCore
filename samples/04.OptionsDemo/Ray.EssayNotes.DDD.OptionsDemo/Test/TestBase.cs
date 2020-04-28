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
        public virtual void Run()
        {
            InitConfiguration();
            InitServiceProvider();
            PrintServiceDescriptors();
            Print();
        }

        /// <summary>
        /// 初始化配置树
        /// </summary>
        protected abstract void InitConfiguration();

        /// <summary>
        /// 初始化依赖注入容器
        /// </summary>
        protected abstract void InitServiceProvider();

        /// <summary>
        /// 打印容器的服务描述池
        /// </summary>
        protected virtual void PrintServiceDescriptors()
        {
            Console.WriteLine($"\r\n容器中的服务描述池：");
            List<ServiceDescriptor> serviceDescriptors = Program.ServiceProvider?.GetServiceDescriptorsFromScope()
                .ToList();

            string re = serviceDescriptors.AsJsonStr(
                option =>
                {
                    option.SerializerSettings = new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        MaxDepth = 1,
                        TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
                    };
                    option.EnumToString = true;
                    option.FilterProps = new FilterPropsOption
                    {
                        FilterEnum = FilterEnum.Ignore,
                        Props = new[] { "Action" }//Action属性内容非常长，忽略掉了
                    };
                }).AsFormatJsonStr();

            Console.WriteLine(re);
        }

        /// <summary>
        /// 打印测试结果
        /// </summary>
        protected abstract void Print();

        /// <summary>
        /// 打印容器的持久化实例池
        /// </summary>
        protected virtual void PrintResolvedServices(IServiceProvider serviceProvider, string name = "")
        {
            Console.WriteLine($"\r\n{name}容器中的持久化实例池：");
            var dic = serviceProvider.GetResolvedServicesFromScope()
                .ToDictionary(x => x.Key.Type.FullName, x => x.Value);
            Console.WriteLine(dic.AsFormatJsonStr(false));
        }
    }
}
