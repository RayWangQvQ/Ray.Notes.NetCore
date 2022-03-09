using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using Xunit;

namespace Ray.EssayNotes.DDD.ConfigurationDemo.ChangeTokenDemo
{
    public class UnitTest01
    {
        [Fact]
        public void Test1()
        {
            Debug.WriteLine("开始监测文件夹");

            string rootPath = Directory.GetCurrentDirectory();
            var phyFileProvider = new PhysicalFileProvider(rootPath);

            IChangeToken changeToken = phyFileProvider.Watch("*.json");
            changeToken.RegisterChangeCallback(_ =>
            {
                CallBack();
            }, "xiaoming");

            Console.ReadLine();

            /*
             * 只能触发一次
             */
        }

        [Fact]
        public void Test2()
        {
            Debug.WriteLine("开始监测文件夹");

            string rootPath = Directory.GetCurrentDirectory();
            var phyFileProvider = new PhysicalFileProvider(rootPath);

            ChangeToken.OnChange(() => phyFileProvider.Watch("*.json"),
                 CallBack);

            Console.ReadLine();

            /*
             * 可以触发多次
             */
        }

        private void CallBack()
        {
            Debug.WriteLine("检测到文件夹有变化!");
        }
    }
}
