using System;
using System.Text.Json;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Ray.EssayNotes.DDD.DbConfigurationDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var initialSettings = new Dictionary<string, string>
            {
                ["Gender"] = "Male",
                ["Age"] = "18",
                ["ContactInfo:EmailAddress"] = "foobar@outlook.com",
                ["ContactInfo:PhoneNo"] = "123456789"
            };

            var configurationRoot = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .AddDatabase("DefaultDb", initialSettings)
                .AddJsonFile("appSettings.json")
                .Build();

            var profile = configurationRoot.Get<Profile>();

            Console.WriteLine(JsonSerializer.Serialize(profile));

            Console.ReadLine();
        }
    }
}
