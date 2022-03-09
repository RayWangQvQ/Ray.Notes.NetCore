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
    public class UnitTest11
    {
        [Fact]
        public void Test1()
        {
            var a = new A();

            ChangeToken.OnChange(
                a.CreateChangeToken,
                CallBack);

            /*
             * 这里传入两个委托（委托A和委托B）
             * 委托A是创建一个IChangeToken
             * 委托B是检测到变更后需要触发的事件
             * 这里能实现持续监控的原因，是因为，每次触发后会自动再去执行一次委托A，从而刷新令牌，实现一直监控
             * 
             * 顺序：第一次先生成IChangeToken，然后将回调委托注册到IChangeToken中，触发变更后，会执行回调委托，此时又会调用委托A去在生成一次IChangeToken......
             */

            Console.ReadLine();
        }

        private void CallBack()
        {
            Debug.WriteLine("接收到了A变更事件");
        }


        public class A
        {
            private CancellationTokenSource _cts;

            public A()
            {
                //模拟5秒后触发
                Task.Run(() =>
                {
                    while (true)
                    {
                        Task.Delay(10000).Wait();
                        Debug.WriteLine("A发生了事件");
                        _cts.Cancel();//其实只是设置IsCancellationRequested为true
                    }
                });
            }

            public IChangeToken CreateChangeToken()
            {
                _cts = new CancellationTokenSource();
                var cct = new CancellationChangeToken(_cts.Token);
                return cct;
            }
        }
    }
}
