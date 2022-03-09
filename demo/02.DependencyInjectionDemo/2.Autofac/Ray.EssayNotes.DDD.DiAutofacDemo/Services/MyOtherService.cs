using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ray.EssayNotes.DDD.DiAutofacDemo.IServices;

namespace Ray.EssayNotes.DDD.DiAutofacDemo.Services
{
    public class MyOtherService : IMyService
    {
        public MyOtherService()
        {
            Console.WriteLine($"Create Instance of MyOtherService：{this.GetHashCode()}");
        }

        public void Test()
        {
            Console.WriteLine("执行Test");
        }
    }
}
