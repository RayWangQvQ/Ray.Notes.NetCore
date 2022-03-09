using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ray.EssayNotes.DDD.DiAutofacDemo.Dtos;
using Ray.EssayNotes.DDD.DiAutofacDemo.Interceptors;
using Ray.EssayNotes.DDD.DiAutofacDemo.IServices;
using Ray.EssayNotes.DDD.DiAutofacDemo.Services;

namespace Ray.EssayNotes.DDD.DiAutofacDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public static ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //Test01(builder);
            //Test02(builder);
            //Test03(builder);
            Test04(builder);
        }

        /// <summary>
        /// ÃüÃû×¢²á
        /// </summary>
        /// <param name="builder"></param>
        public void Test01(ContainerBuilder builder)
        {
            builder.RegisterType<MyService>()
                .As<IMyService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<MyOtherService>()
                .Named<IMyService>("other")
                .InstancePerLifetimeScope();
        }

        /// <summary>
        /// ÊôÐÔ×¢Èë
        /// </summary>
        /// <param name="builder"></param>
        public void Test02(ContainerBuilder builder)
        {
            builder.RegisterType<MyDto>();

            builder.RegisterType<MyService>()
                .PropertiesAutowired()
                .InstancePerLifetimeScope();
        }

        /// <summary>
        /// AOP
        /// </summary>
        /// <param name="builder"></param>
        public void Test03(ContainerBuilder builder)
        {
            builder.RegisterType<MyInterceptor>();

            builder.RegisterType<MyOtherService>()
                .AsImplementedInterfaces()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(MyInterceptor), typeof(MyInterceptor))
                .InstancePerLifetimeScope();
        }

        /// <summary>
        /// AOP
        /// </summary>
        /// <param name="builder"></param>
        public void Test04(ContainerBuilder builder)
        {
            builder.RegisterType<MyDto>()
                .InstancePerMatchingLifetimeScope("myScope");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutofacContainer = app.ApplicationServices.GetAutofacRoot();

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
