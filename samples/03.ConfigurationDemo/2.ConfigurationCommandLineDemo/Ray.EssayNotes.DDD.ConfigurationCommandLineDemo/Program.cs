using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Ray.EssayNotes.DDD.ConfigurationCommandLineDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();

            //Test1(builder, args);
            Test2(builder, args);

            IConfigurationRoot configurationRoot = builder.Build();

            Console.WriteLine($"Key1：{configurationRoot["CommandLineKey1"]}");
            Console.WriteLine($"Key2：{configurationRoot["CommandLineKey2"]}");
            Console.WriteLine($"Key3：{configurationRoot["CommandLineKey3"]}");
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
                {"-c","CommandLineKey1" },
                { "#d","CommandLineKey2"}
            };
            builder.AddCommandLine(args, mapper);
        }
    }
}
