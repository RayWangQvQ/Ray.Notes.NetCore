using System;
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
                Console.WriteLine($"请输入测试编号:{TestFactory.Selections.AsFormatJsonStr()}");
                string num = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(num)) continue;

                TestBase test = TestFactory.Create(num);
                test.Run();
            }
        }
    }
}
