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
    public class UnitTest20
    {
        [Fact]
        public void Test1()
        {
            Debug.WriteLine("开始监测");

            var watch = new MyWatcher();
            ChangeToken.OnChange(
                () => watch.CreateChangeToken(),
                 CallBack);

            Console.ReadLine();
        }

        private void CallBack()
        {
            Debug.WriteLine("检测到配置有变化!");
        }
    }

    public class MyWatcher
    {
        private CancellationTokenSource _cts;

        public MyWatcher()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    Task.Delay(5000).Wait();
                    Debug.WriteLine("配置变了");
                    ConfigChanged();
                }
            });
        }

        public IChangeToken CreateChangeToken()
        {
            _cts = new CancellationTokenSource();
            var ct = new CancellationChangeToken(_cts.Token);
            return ct;
        }

        public void ConfigChanged()
        {
            _cts.Cancel();
        }
    }
}
