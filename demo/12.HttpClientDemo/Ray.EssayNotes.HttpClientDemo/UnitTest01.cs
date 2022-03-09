using System;
using System.Diagnostics;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Ray.EssayNotes.HttpClientDemo
{
    /// <summary>
    /// ��һ��ʹ�÷�����ֱ��ʹ��HttpClient
    /// </summary>
    public class UnitTest01
    {
        private IHost _host;

        public UnitTest01()
        {
            _host = CreateHostBuilder(null).Build();
        }

        [Fact]
        public void Test1()
        {
            using (var scope = _host.Services.CreateScope())
            {
                var sp = scope.ServiceProvider;
                var client = sp.GetRequiredService<HttpClient>();
                var result = client.GetStringAsync(Constant.GetIpApi)
                    .GetAwaiter().GetResult();
                Debug.WriteLine(result);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args);

            builder.ConfigureServices(s =>
            {
                s.AddHttpClient();
                /*
                 * �ڲ���ע��HttpClient
                 * services.TryAddTransient((IServiceProvider s) => s.GetRequiredService<IHttpClientFactory>().CreateClient(string.Empty));
                 * ����������˲ʱʵ��
                 * �䱾����ͨ��IHttpClientFactory����
                 */
            });

            return builder;
        }
    }
}
