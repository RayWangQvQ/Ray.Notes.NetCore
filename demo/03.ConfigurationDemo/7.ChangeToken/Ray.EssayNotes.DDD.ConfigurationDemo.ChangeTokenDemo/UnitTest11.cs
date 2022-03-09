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
             * ���ﴫ������ί�У�ί��A��ί��B��
             * ί��A�Ǵ���һ��IChangeToken
             * ί��B�Ǽ�⵽�������Ҫ�������¼�
             * ������ʵ�ֳ�����ص�ԭ������Ϊ��ÿ�δ�������Զ���ȥִ��һ��ί��A���Ӷ�ˢ�����ƣ�ʵ��һֱ���
             * 
             * ˳�򣺵�һ��������IChangeToken��Ȼ�󽫻ص�ί��ע�ᵽIChangeToken�У���������󣬻�ִ�лص�ί�У���ʱ�ֻ����ί��Aȥ������һ��IChangeToken......
             */

            Console.ReadLine();
        }

        private void CallBack()
        {
            Debug.WriteLine("���յ���A����¼�");
        }


        public class A
        {
            private CancellationTokenSource _cts;

            public A()
            {
                //ģ��5��󴥷�
                Task.Run(() =>
                {
                    while (true)
                    {
                        Task.Delay(10000).Wait();
                        Debug.WriteLine("A�������¼�");
                        _cts.Cancel();//��ʵֻ������IsCancellationRequestedΪtrue
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
