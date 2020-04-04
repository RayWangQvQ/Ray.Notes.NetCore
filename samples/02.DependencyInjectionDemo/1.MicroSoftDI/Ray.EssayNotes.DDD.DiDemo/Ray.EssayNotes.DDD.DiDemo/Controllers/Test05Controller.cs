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
    /// 利用注册委托实现属性注入
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class Test05Controller : ControllerBase
    {
        private readonly MyDto _myDto1;
        private readonly OtherDto _otherDto;

        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="myDto1"></param>
        /// <param name="myDto2"></param>
        public Test05Controller(MyDto myDto1,
            OtherDto otherDto)
        {
            _myDto1 = myDto1;
            _otherDto = otherDto;
        }

        [HttpGet]
        public bool Get()
        {
            Console.WriteLine($"_myDto1:{_myDto1.GetHashCode()}");
            Console.WriteLine($"_myDto2:{_otherDto.GetHashCode()}");
            Console.WriteLine($"_myDto2.MyDto:{_otherDto.MyDto.GetHashCode()}");

            Console.WriteLine($"========请求结束=======");

            return true;
        }
    }
}