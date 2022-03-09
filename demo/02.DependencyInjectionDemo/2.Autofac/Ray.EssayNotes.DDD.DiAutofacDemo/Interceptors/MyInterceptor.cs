using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace Ray.EssayNotes.DDD.DiAutofacDemo.Interceptors
{
    public class MyInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine($"进入【{invocation.Method.Name}】之前");
            invocation.Proceed();
            Console.WriteLine($"【{invocation.Method.Name}】执行完毕");
        }
    }
}
