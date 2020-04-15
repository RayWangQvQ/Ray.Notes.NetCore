using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ray.EssayNotes.DDD.OptionsDemo.Test
{
    public class TestFactory
    {
        public TestBase Create(string num)
        {
            return Assembly.GetExecutingAssembly()
                .CreateInstance($"Ray.EssayNotes.DDD.OptionsDemo.Test.Test{num}")
                as TestBase;
        }

        public Dictionary<string, string> Selections =>
            Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.Name.Contains("Test")
                    && !x.IsAbstract
                    && !x.Name.Contains("Factory"))
                .ToDictionary(x => x.Name.Substring(x.Name.Length - 2),
                    x => x.GetCustomAttribute<DescriptionAttribute>()?.Description ?? "");
    }
}
