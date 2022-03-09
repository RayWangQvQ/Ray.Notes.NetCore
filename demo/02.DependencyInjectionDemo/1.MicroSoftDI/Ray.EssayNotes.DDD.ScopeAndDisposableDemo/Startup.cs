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
            //Test02(services);
            //Test03(services);
            //Test04(services);
            Test05(services);
        }

        /// <summary>
        /// ����˲ʱʵ�����ͷ�
        /// </summary>
        /// <param name="services"></param>
        private void Test01(IServiceCollection services)
        {
            services.AddTransient<IOrderService, OrderService>();
        }

        /// <summary>
        /// �������ڵ���ʵ���ͷ�
        /// </summary>
        /// <param name="services"></param>
        private void Test02(IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();
        }

        /// <summary>
        /// ����ȫ�ֵ���
        /// </summary>
        /// <param name="services"></param>
        private void Test03(IServiceCollection services)
        {
            //services.AddSingleton<IOrderService, OrderService>();
            services.AddSingleton<IOrderService>(x => new OrderService());
        }

        /// <summary>
        /// ����ȫ�ֵ���02
        /// </summary>
        /// <param name="services"></param>
        private void Test04(IServiceCollection services)
        {
            Test03(services);
        }

        /// <summary>
        /// ����ȫ�ֵ���03���Լ�new����ʵ��ע�ᣬ�ڸ����������᲻�ᱻ�ͷ�
        /// </summary>
        /// <param name="services"></param>
        private void Test05(IServiceCollection services)
        {
            var instance = new OrderService();
            services.AddSingleton<IOrderService>(instance);

            var test = services.FirstOrDefault(x => x.ServiceType == typeof(IOrderService));
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /**ϵͳĬ�ϵ�����������˵scpoe������ֻ������
             * ApplicationServices������������ϵͳ����ʱ����
             * RequestService�������������ڳ���ÿ�ν��ܵ�request���������Ӧ������м������������������ͷŵ�
             */
            ServiceProviderRoot = app.ApplicationServices;//�õ�������
            //ServiceProviderRoot.GetService<>();//���ԴӸ���������ʵ��

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
