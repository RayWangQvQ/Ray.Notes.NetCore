using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ray.EssayNotes.DDD.ScopeAndDisposableDemo.IServices;
using Ray.EssayNotes.DDD.ScopeAndDisposableDemo.Services;
using System.Text.Json;

namespace Ray.EssayNotes.DDD.ScopeAndDisposableDemo
{
    public class Startup
    {
        public static IServiceProvider ServiceProviderRoot;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //Test01(services);
            Test02(services);
            //Test03(services);
            //Test04(services);
        }

        /// <summary>
        /// 测试瞬时实例的释放
        /// </summary>
        /// <param name="services"></param>
        private void Test01(IServiceCollection services)
        {
            services.AddTransient<IOrderService, OrderService>();
        }

        /// <summary>
        /// 测试域内单例实例释放
        /// </summary>
        /// <param name="services"></param>
        private void Test02(IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();
        }

        private void Test03(IServiceCollection services)
        {
            //services.AddSingleton<IOrderService, OrderService>();
            services.AddSingleton<IOrderService>(x => new OrderService());
        }

        /// <summary>
        /// 测试自己new出的实例注册，在跟容器解析会不会被释放
        /// </summary>
        /// <param name="services"></param>
        private void Test04(IServiceCollection services)
        {
            var instance = new OrderService();
            services.AddSingleton<IOrderService>(instance);

            var test = services.FirstOrDefault(x => x.ServiceType == typeof(IOrderService));
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /**系统默认的容器（或者说scpoe作用域）只有两种
             * ApplicationServices：根容器，在系统启动时创建
             * RequestService：请求容器，在程序每次接受到request请求后，由相应负责的中间件创建，请求结束后即释放掉
             */
            ServiceProviderRoot = app.ApplicationServices;//拿到根容器
            //ServiceProviderRoot.GetService<>();//可以从跟容器解析实例

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
