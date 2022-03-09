using System;
using System.Diagnostics;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Ray.EssayNotes.HttpClientDemo
{
    /// <summary>
    /// 第一种使用方法：直接使用HttpClient
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
                 * 内部会注册HttpClient
                 * services.TryAddTransient((IServiceProvider s) => s.GetRequiredService<IHttpClientFactory>().CreateClient(string.Empty));
                 * 生命周期是瞬时实例
                 * 其本质是通过IHttpClientFactory创建
                 */
            });

            return builder;
        }
    }
}
