using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Ray.EssayNotes.DDD.ConfigurationDemo.Test;
using System.Text.Json;
using Ray.Infrastructure.Extensions;

namespace Ray.EssayNotes.DDD.ConfigurationDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(JsonSerializer.Serialize(args));
            while (true)
            {
                var factory = new TestFactory();

                Console.WriteLine($"\r\n请输入测试编号：{JsonSerializer.Serialize(factory.TestSections).AsFormatJsonString()}");
                string num = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(num)) continue;

                ITest test = factory.Create(num);
                test.Run();
            }
        }
    }
}
