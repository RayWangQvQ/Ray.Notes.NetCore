using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using Xunit;

namespace Ray.EssayNotes.DDD.ConfigurationDemo.ChangeTokenDemo
{
    public class UnitTest10
    {
        [Fact]
        public void Test1()
        {
            var cts = new CancellationTokenSource();
            var cct = new CancellationChangeToken(cts.Token);

            var a = new A(cts);
            var b = new B(cct);

            Console.ReadLine();
        }


        public class A
        {
            private CancellationTokenSource _cts;

            public A(CancellationTokenSource cts)
            {
                this._cts = cts;

                //模拟5秒后触发
                Task.Run(() =>
                {
                    Task.Delay(5000).Wait();
                    Debug.WriteLine("A发生了事件");
                    _cts.Cancel();//其实只是设置IsCancellationRequested为true
                });
            }
        }

        public class B
        {
            public B(CancellationChangeToken cct)
            {
                cct.RegisterChangeCallback(obj =>
                {
                    Debug.WriteLine("B接收到了事件");
                },
                    "test");
            }
        }
    }
}
