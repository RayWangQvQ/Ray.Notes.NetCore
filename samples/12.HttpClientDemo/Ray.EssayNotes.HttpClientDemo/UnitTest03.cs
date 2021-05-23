using System;
using System.Diagnostics;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Ray.EssayNotes.HttpClientDemo
{
    /// <summary>
    /// 第三种使用方法：使用命令的HttpClient
    /// </summary>
    public class UnitTest03
    {
        private IHost _host;

        private const string _getIpApiClientName = "GetIpApi";

        public UnitTest03()
        {
            _host = CreateHostBuilder(null).Build();
        }

        [Fact]
        public void Test1()
        {
            using (var scope = _host.Services.CreateScope())
            {
                var sp = scope.ServiceProvider;
                var factory = sp.GetRequiredService<IHttpClientFactory>();
                var client = factory.CreateClient(_getIpApiClientName);//创建的时候可以传入名称
                var result = client.GetStringAsync("").GetAwaiter().GetResult();
                Debug.WriteLine(result);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args);

            builder.ConfigureServices(s =>
            {
                s.AddHttpClient();
                s.AddHttpClient(_getIpApiClientName, client =>
                 {
                     client.DefaultRequestHeaders.Add("clent-name", "namedCient");
                     client.BaseAddress = new Uri(Constant.GetIpApi);
                 });
            });

            return builder;
        }
    }
}
