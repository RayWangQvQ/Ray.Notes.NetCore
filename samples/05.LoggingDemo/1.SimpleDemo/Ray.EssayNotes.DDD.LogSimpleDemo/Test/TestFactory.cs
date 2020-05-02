using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ray.EssayNotes.DDD.LogSimpleDemo.Test
{
    public static class TestFactory
    {
        public static TestBase Create(string num)
        {
            return (TestBase)Assembly.GetExecutingAssembly()
                .CreateInstance($"Ray.EssayNotes.DDD.LogSimpleDemo.Test.Test{num}");
        }

        public static Dictionary<string, string> Selections => Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(x => x.BaseType == typeof(TestBase))
            .ToDictionary(x => x.Name.Substring(x.Name.Length - 2),
            x => x.GetCustomAttribute<DescriptionAttribute>()?.Description);
    }
}
