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
            Debug.WriteLine("��ʼ����ļ���");

            string rootPath = Directory.GetCurrentDirectory();
            var phyFileProvider = new PhysicalFileProvider(rootPath);

            IChangeToken changeToken = phyFileProvider.Watch("*.json");
            changeToken.RegisterChangeCallback(_ =>
            {
                CallBack();
            }, "xiaoming");

            Console.ReadLine();

            /*
             * ֻ�ܴ���һ��
             */
        }

        [Fact]
        public void Test2()
        {
            Debug.WriteLine("��ʼ����ļ���");

            string rootPath = Directory.GetCurrentDirectory();
            var phyFileProvider = new PhysicalFileProvider(rootPath);

            ChangeToken.OnChange(() => phyFileProvider.Watch("*.json"),
                 CallBack);

            Console.ReadLine();

            /*
             * ���Դ������
             */
        }

        private void CallBack()
        {
            Debug.WriteLine("��⵽�ļ����б仯!");
        }
    }
}
