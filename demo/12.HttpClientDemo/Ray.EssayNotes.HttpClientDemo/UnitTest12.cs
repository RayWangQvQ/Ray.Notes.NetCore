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
    /// 特性：配置Handler的生命周期时间
    /// </summary>
    public class UnitTest12
    {
        private IHost _host;

        public UnitTest12()
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
                .SetHandlerLifetime(TimeSpan.FromSeconds(1));//配置Handler的生命周期,默认2分钟
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
    }
}
