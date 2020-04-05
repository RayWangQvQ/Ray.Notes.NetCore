using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Ray.EssayNotes.DDD.ScopeAndDisposableDemo.IServices;
using Ray.Infrastructure.Extensions;

namespace Ray.EssayNotes.DDD.ScopeAndDisposableDemo.Controllers
{
    /// <summary>
    /// 测试瞬时实例的释放
    /// </summary>
    public abstract class MyControllerBase : ControllerBase
    {
        /// <summary>
        /// 打印根容器中持久化实例池与可释放实例池
        /// </summary>
        protected void PrintFromRootScope()
        {
            PrintInstancePool(Startup.ServiceProviderRoot);
            PrintDisposablePool(Startup.ServiceProviderRoot);
        }

        /// <summary>
        /// 打印请求域中持久化实例池与可释放实例池
        /// </summary>
        protected void PrintFromRequestServiceScope()
        {
            PrintInstancePool(HttpContext.RequestServices);
            PrintDisposablePool(HttpContext.RequestServices);
        }

        /// <summary>
        /// 打印容器中持久化实例池
        /// </summary>
        /// <param name="serviceProvider"></param>
        private void PrintInstancePool(IServiceProvider serviceProvider)
        {
            Console.Write($"{serviceProvider}中持久化实例池内容：");
            var dic = serviceProvider.GetInstanceNamesFromScope();
            Console.WriteLine(JsonConvert.SerializeObject(dic).AsFormatJsonString());
        }

        /// <summary>
        /// 打印容器中可释放实例池
        /// </summary>
        /// <param name="serviceProvider"></param>
        private void PrintDisposablePool(IServiceProvider serviceProvider)
        {
            Console.Write($"{serviceProvider}中可释放实例池内容：");
            var list = serviceProvider.GetDisposableCoponentNamesFromScope();
            Console.WriteLine(JsonConvert.SerializeObject(list).AsFormatJsonString());
        }
    }
}