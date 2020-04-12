using System;
using System.Collections.Generic;
using Autofac;
using Microsoft.AspNetCore.Mvc;
using Ray.EssayNotes.DDD.DiAutofacDemo.IServices;

namespace Ray.EssayNotes.DDD.DiAutofacDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Test01Controller : ControllerBase
    {
        private readonly IEnumerable<IMyService> _myServices;

        public Test01Controller(IEnumerable<IMyService> myServices)
        {
            this._myServices = myServices;
        }

        [HttpGet]
        public bool Get()
        {
            foreach (var item in _myServices)
            {
                Console.WriteLine($"{item.GetHashCode()}");
            }

            var namedService = Startup.AutofacContainer.ResolveNamed<IMyService>("other");
            Console.WriteLine($"{namedService.GetHashCode()}");
            return true;
        }
    }
}
