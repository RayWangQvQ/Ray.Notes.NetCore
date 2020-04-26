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
        /// 打印测试结果
        /// </summary>
        public abstract void Print();

        /// <summary>
        /// 打印容器信息
        /// </summary>
        public virtual void PrintContainerInfo()
        {
            Console.WriteLine("\r\n容器中的服务描述池：");
            var serviceDescriptors = Program.ServiceProvider.GetServiceDescriptorsFromScope()
                .ToList();
            serviceDescriptors.ForEach(Console.WriteLine);

            Console.WriteLine("\r\n容器中的持久化实例池：");
            var dic = Program.ServiceProvider.GetResolvedServicesFromScope();
            Console.WriteLine(JsonSerializer.Serialize(dic).AsFormatJsonStr());
        }

        public virtual void Run()
        {
            InitConfiguration();
            InitServiceProvider();
            Print();
            PrintContainerInfo();
        }
    }
}
