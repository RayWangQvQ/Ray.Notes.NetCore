using System;
using Microsoft.Extensions.Configuration;

namespace Ray.EssayNotes.DDD.ConfigurationEnvironmentVariablesDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Test1();
            Test2();
        }

        private static void Test1()
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            Console.WriteLine(configurationRoot["TestKey1"]);

            Console.WriteLine(configurationRoot.GetSection("Section1")["Key3"]);
        }

        private static void Test2()
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .AddEnvironmentVariables("Pre_")
                .Build();

            /**
             * 这儿只会加载含有该前缀的配置项
             */

            Console.WriteLine(configurationRoot["TestKey1"]);

            Console.WriteLine(configurationRoot.GetSection("Section1")["Key3"]);

            Console.WriteLine(configurationRoot["Key4"]);
        }
    }
}
