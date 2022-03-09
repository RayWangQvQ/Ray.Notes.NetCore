using System;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace MiddlewareDemo
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder();
        }
    }
}
