using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ray.EssayNotes.DDD.ScopeAndDisposableDemo.IServices;

namespace Ray.EssayNotes.DDD.ScopeAndDisposableDemo.Services
{
    public class OrderService : IOrderService, IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine($"释放：{this.GetHashCode()}");
        }
    }
}
