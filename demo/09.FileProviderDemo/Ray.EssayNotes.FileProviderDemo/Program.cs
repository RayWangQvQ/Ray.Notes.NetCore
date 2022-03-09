using System;
using System.IO;
using System.Net.NetworkInformation;
using Microsoft.Extensions.FileProviders;
using Ray.EssayNotes.FileProviderDemo.Test;

namespace Ray.EssayNotes.FileProviderDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine($"请输入测试编号：{TestFactory.Selections.AsFormatJsonStr()}");

                string num = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(num)) continue;

                TestBase test = TestFactory.Create(num);
                test.Run();
            }

        }
    }
}
