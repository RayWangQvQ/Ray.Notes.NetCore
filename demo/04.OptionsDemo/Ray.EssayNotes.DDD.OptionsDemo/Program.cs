using System;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ray.EssayNotes.DDD.OptionsDemo.Test;

namespace Ray.EssayNotes.DDD.OptionsDemo
{
    class Program
    {
        public static IConfigurationRoot ConfigurationRoot;

        public static IServiceProvider ServiceProvider;

        static void Main(string[] args)
        {
            while (true)
            {
                TestFactory factory = new TestFactory();

                Console.WriteLine($"\r\n请输入测试编号：{JsonSerializer.Serialize(factory.Selections).AsFormatJsonStr()}");

                string num = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(num)) continue;

                TestBase test = factory.Create(num);
                test.Run();
            }
        }
    }
}
