using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ray.EssayNotes.DDD.DiAutofacDemo.Dtos
{
    public class MyDto
    {
        public MyDto()
        {
            Console.WriteLine($"Create instance of MyDto：{this.GetHashCode()}");
        }
    }
}
