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

                //ģ��5��󴥷�
                Task.Run(() =>
                {
                    Task.Delay(5000).Wait();
                    Debug.WriteLine("A�������¼�");
                    _cts.Cancel();//��ʵֻ������IsCancellationRequestedΪtrue
                });
            }
        }

        public class B
        {
            public B(CancellationChangeToken cct)
            {
                cct.RegisterChangeCallback(obj =>
                {
                    Debug.WriteLine("B���յ����¼�");
                },
                    "test");
            }
        }
    }
}
