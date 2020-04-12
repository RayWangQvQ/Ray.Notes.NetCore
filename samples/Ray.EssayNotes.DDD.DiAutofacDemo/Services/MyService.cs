using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ray.EssayNotes.DDD.DiAutofacDemo.Dtos;
using Ray.EssayNotes.DDD.DiAutofacDemo.IServices;

namespace Ray.EssayNotes.DDD.DiAutofacDemo.Services
{
    public class MyService : IMyService
    {
        public MyService()
        {
            Console.WriteLine($"Create Instance of MyService：{this.GetHashCode()}");
        }

        public MyDto MyDto { get; set; }

        public void Test()
        {
            throw new NotImplementedException();
        }
    }
}
