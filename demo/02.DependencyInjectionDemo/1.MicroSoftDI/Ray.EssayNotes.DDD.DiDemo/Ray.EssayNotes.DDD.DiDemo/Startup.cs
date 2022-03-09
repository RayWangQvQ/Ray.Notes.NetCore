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
using Ray.EssayNotes.DDD.DiDemo.Dtos;
using Ray.EssayNotes.DDD.DiDemo.IServices;
using Ray.EssayNotes.DDD.DiDemo.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Ray.EssayNotes.DDD.DiDemo
{
    public class Startup
    {
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
            //Test05(services);
            //Test06(services);
            //Test07(services);
            //Test08(services);
            Test09(services);
        }

        /// <summary>
        /// ���Թ���ע��
        /// </summary>
        private void Test01(IServiceCollection services)
        {
            services.AddTransient<IMyTransientService, MyTransientService>();
            services.AddScoped<IMyScopedService, MyScopedService>();
            services.AddSingleton<IMySingletonService, MySingletonService>();
        }

        /// <summary>
        /// ���Բ���ע��
        /// </summary>
        private void Test02(IServiceCollection services)
        {
            Test01(services);
        }

        /// <summary>
        /// ����newʵ��ע��
        /// </summary>
        private void Test03(IServiceCollection services)
        {
            var dto = new MyDto();

            //ֻ��ע�ᵥ���������ڲ���ֱ��ʹ����ʵ�����Ķ������ǻὫʵ������ֱ�ӳ־û�������
            //��������д����ͬЧ��
            services.AddSingleton(dto);
            services.AddSingleton<MyDto>(dto);

            //����д�������ڣ���������Ϊ˲ʱ�����ڵ����ģ�ע��ʱֻ���ṩ���췽����ί�У�����ֱ��ע��ʵ��
            //services.AddTransient(dto);
            //services.AddScoped(dto);
        }

        /// <summary>
        /// ί��ע��
        /// </summary>
        private void Test04(IServiceCollection services)
        {
            services.AddScoped(serviceProvider => new MyDto());
        }

        /// <summary>
        /// ����ע��ί��ʵ������ע��
        /// </summary>
        private void Test05(IServiceCollection services)
        {
            //services.AddScoped(serviceProvider => new MyDto());

            services.AddScoped(serviceProvider => new OtherDto
            {
                MyDto = serviceProvider.GetService<MyDto>()
            });
        }

        /// <summary>
        /// ����ע��
        /// </summary>
        private void Test06(IServiceCollection services)
        {
            Test01(services);
            services.TryAddScoped<IMyScopedService, OtherScopedService>();//����������Ѵ���IMyScopedService���Ͳ�ע�ᣬ��������ڣ�������ע��

            //��������д��������Ч����ͬ
            //services.TryAdd(new ServiceDescriptor(typeof(IMyScopedService), x => new OtherScopedService(), ServiceLifetime.Scoped));
            //services.TryAdd(ServiceDescriptor.Scoped<IMyScopedService, OtherScopedService>());
        }

        /// <summary>
        /// ����ע���ʵ�ַ���
        /// </summary>
        private void Test07(IServiceCollection services)
        {
            Test01(services);

            services.TryAddEnumerable(ServiceDescriptor.Scoped<IMyScopedService, OtherScopedService>());//��������ʵ�����Ͷ���ͬ���Ͳ����ٴ�ע�᣻�����ͬ���ͻ�ע��
        }

        /// <summary>
        /// �Ƴ�/�滻
        /// </summary>
        private void Test08(IServiceCollection services)
        {
            Test01(services);

            services.Replace(ServiceDescriptor.Scoped<IMyScopedService, OtherScopedService>());

            services.RemoveAll<IMyScopedService>();
        }

        /// <summary>
        /// ע�᷺��
        /// </summary>
        private void Test09(IServiceCollection services)
        {
            Test01(services);

            services.AddScoped(typeof(GenericDto<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
