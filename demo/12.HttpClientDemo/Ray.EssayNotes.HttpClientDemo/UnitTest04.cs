using System;
using System.Diagnostics;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Ray.EssayNotes.HttpClientDemo
{
    /// <summary>
    /// ������ʹ�÷�����ʹ������HttpClient
    /// </summary>
    public class UnitTest04
    {
        private IHost _host;

        public UnitTest04()
        {
            _host = CreateHostBuilder(null).Build();
        }

        [Fact]
        public void Test1()
        {
            using (var scope = _host.Services.CreateScope())
            {
                var sp = scope.ServiceProvider;
                var api = sp.GetRequiredService<GetIpApi>();
                var result = api.GetIp();
                Debug.WriteLine(result);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args);

            builder.ConfigureServices(s =>
            {
                s.AddHttpClient<GetIpApi>(client =>
                {
                    client.DefaultRequestHeaders.Add("clent-name", "namedCient");
                });
            });

            return builder;
        }

        public class GetIpApi
        {
            private readonly HttpClient client;

            public GetIpApi(HttpClient client)//�����HttpClient��ע������ע���
            {
                this.client = client;
            }

            public string GetIp()
            {
                return this.client.GetStringAsync(Constant.GetIpApi)
                    .GetAwaiter().GetResult();
            }
        }
    }
}
