using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ray.EssayNotes.DDD.DiDemo.Dtos;
using Ray.EssayNotes.DDD.DiDemo.IServices;

namespace Ray.EssayNotes.DDD.DiDemo.Controllers
{
    /// <summary>
    /// 注册泛型
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class Test09Controller : ControllerBase
    {
        private readonly GenericDto<MyDto> _genericDto;
        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="myTransientService"></param>
        /// <param name="myScopedService"></param>
        /// <param name="mySingletonService"></param>
        public Test09Controller(GenericDto<MyDto> genericDto)
        {
            _genericDto = genericDto;
        }

        [HttpGet]
        public bool Get()
        {
            Console.WriteLine($"_genericDto:{_genericDto.GetHashCode()}");
            Console.WriteLine($"_genericDto.InnerDto.:{_genericDto.GetType()}");

            Console.WriteLine($"========请求结束=======");

            return true;
        }
    }
}