using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ray.EssayNotes.DDD.LogSimpleDemo.Test;

namespace Ray.EssayNotes.DDD.LogSimpleDemo
{
    class Program
    {
        public static IConfigurationRoot ConfigurationRoot { get; set; }

        public static IServiceProvider ServiceProviderRoot { get; set; }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine($"\r\n请输入测试编号:{TestFactory.Selections.AsFormatJsonStr()}");
                string num = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(num)) continue;

                TestBase test = TestFactory.Create(num);
                test.Run();
                System.Threading.Thread.Sleep(1000);//Console.Write是异步的，所以主线程等待1秒，避免控制台交叉输出
                //Debug.WriteLine("测试异步");
            }
        }
    }
}
