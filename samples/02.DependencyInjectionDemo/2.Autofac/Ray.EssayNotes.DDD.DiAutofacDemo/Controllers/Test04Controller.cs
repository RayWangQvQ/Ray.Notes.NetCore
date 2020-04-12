using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ray.EssayNotes.DDD.DiAutofacDemo.Dtos;
using Ray.EssayNotes.DDD.DiAutofacDemo.IServices;

namespace Ray.EssayNotes.DDD.DiAutofacDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Test04Controller : ControllerBase
    {
        [HttpGet]
        public bool Get()
        {
            using (var scope = Startup.AutofacContainer.BeginLifetimeScope("myScope"))
            {
                var instance = scope.Resolve<MyDto>();
                Console.WriteLine($"{instance.GetHashCode()}");

                using (var subScope = scope.BeginLifetimeScope())
                {
                    var instance2 = scope.Resolve<MyDto>();
                    Console.WriteLine($"{instance2.GetHashCode()}");

                    var instance3 = scope.Resolve<MyDto>();
                    Console.WriteLine($"{instance3.GetHashCode()}");
                }
            }

            return true;
        }
    }
}
