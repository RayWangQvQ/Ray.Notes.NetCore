using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Ray.EssayNotes.DDD.ConfigurationCommandLineDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"项目已启动，入参：{JsonSerializer.Serialize(args)}");

            var builder = new ConfigurationBuilder();

            //Test1(builder, args);
            Test2(builder, args);

            IConfigurationRoot configurationRoot = builder.Build();

            Console.WriteLine($"TestKey1：{configurationRoot["TestKey1"]}");
            Console.WriteLine($"TestKey2：{configurationRoot["TestKey2"]}");
            Console.WriteLine($"TestKey3：{configurationRoot["TestKey3"]}");
        }

        public static void Test1(ConfigurationBuilder builder, string[] args)
        {
            builder.AddCommandLine(args);
        }

        /// <summary>
        /// 替换
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="args"></param>
        public static void Test2(ConfigurationBuilder builder, string[] args)
        {
            var mapper = new Dictionary<string, string>
            {
                {"-tk1","TestKey1" },
            };
            builder.AddCommandLine(args, mapper);
        }
    }
}
