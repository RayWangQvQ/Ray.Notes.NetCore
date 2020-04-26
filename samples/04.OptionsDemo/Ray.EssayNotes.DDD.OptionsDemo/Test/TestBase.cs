using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

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
            var serviceDescriptors = Program.ServiceProvider?.GetServiceDescriptorsFromScope()
                .ToList();
            serviceDescriptors?.ForEach(Console.WriteLine);
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
                .ToList();
            dic.ForEach(x => Console.WriteLine($"{x}:{x.AsFormatJsonStr()}"));
        }
    }
}
