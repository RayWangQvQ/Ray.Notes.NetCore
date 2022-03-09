using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Ray.EssayNotes.HttpClientDemo
{
    /// <summary>
    /// ���ԣ���������ܵ�
    /// </summary>
    public class UnitTest11
    {
        private IHost _host;

        public UnitTest11()
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
                })
                .AddHttpMessageHandler<RequestIdDelegatingHandler>();

                s.AddTransient<RequestIdDelegatingHandler>();//ע��,handlerҪ�Լ�ע��
            });

            return builder;
        }

        public class GetIpApi
        {
            private readonly HttpClient client;

            public GetIpApi(HttpClient client)
            {
                this.client = client;
            }

            public string GetIp()
            {
                return this.client.GetStringAsync(Constant.GetIpApi)
                    .GetAwaiter().GetResult();
            }
        }

        public class RequestIdDelegatingHandler : DelegatingHandler
        {
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                //��������
                request.Headers.Add("x-guid", Guid.NewGuid().ToString());

                var result = await base.SendAsync(request, cancellationToken); //�����ڲ�handler

                //������Ӧ

                return result;
            }

            //�����м��������ܵ�ģ��
        }
    }
}
