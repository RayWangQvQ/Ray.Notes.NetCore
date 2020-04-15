using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace Ray.EssayNotes.DDD.ConfigurationDIY
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Test1();
            Test2();
            Console.ReadLine();
        }

        private static void Test1()
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .Add(new MyConfigurationSource())
                .Build();

            Console.WriteLine($"laseTime：{configurationRoot["lastTime"]}");
        }

        private static void Test2()
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .Add(new MyConfigurationSource())
                .Build();

            ChangeToken.OnChange(() => configurationRoot.GetReloadToken(), () =>
            {
                Console.WriteLine($"lastTime:{configurationRoot["lastTime"]}");
            });
        }
    }
}
