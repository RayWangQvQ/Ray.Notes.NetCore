using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace Ray.EssayNotes.DDD.ConfigurationDemo.Test
{
    public class TestFactory
    {
        public ITest Create(string num)
        {
            string classFullName = $"Ray.EssayNotes.DDD.ConfigurationDemo.Test.Test{num}";
            Assembly assembly = Assembly.GetExecutingAssembly();
            return assembly.CreateInstance(classFullName) as ITest;
        }

        public Dictionary<string, string> TestSections =>
            Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.GetInterface("ITest") != null
                            && x.IsClass)
                .ToDictionary(type => type.Name.Substring(type.Name.Length - 2),
                    type => type.GetCustomAttribute<DescriptionAttribute>()?.Description);
    }
}
