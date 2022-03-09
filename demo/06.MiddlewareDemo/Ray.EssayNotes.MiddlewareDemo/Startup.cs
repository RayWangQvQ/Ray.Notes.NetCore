using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Ray.EssayNotes.MiddlewareDemo
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Test1(app, env);
            //Test2(app, env);
            //Test3(app, env);
            //Test4(app, env);
            //Test5(app, env);
            Test6(app, env);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //��ӡ���м����·
            var components = app.GetFieldValue("_components");
            var jsonStr = components.AsJsonStr(option =>
            {
                option.SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                };
                option.FilterProps = new Infrastructure.Extensions.Json.FilterPropsOption
                {
                    FilterEnum = Infrastructure.Extensions.Json.FilterEnum.Retain,
                    Props = new[] { "Target", "middleware" }
                };
            }).AsFormatJsonStr();
            Console.WriteLine($"��ǰ�м����{jsonStr}");
        }

        /// <summary>
        /// ����м���������
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Test1(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("����Test1");
            });

            /**
             * ��������http���󷵻�ֵ
             * ��������ʲôapi�����᷵��������
             * ��Ϊû�ж�ִ�к����м��������Ҳ����ִ�к�������
             */
        }

        /// <summary>
        /// ����м������ִ�к����м��
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Test2(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("����Test2");
                await next();
            });

            /**
             * ����ᱨ�쳣����Ϊ�����м����Header�в���
             */
        }
        /// <summary>
        /// �ж�header
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Test3(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.HasStarted)
                    await context.Response.WriteAsync("����Test3");
            });

            /**
             * ���headerû�з��أ���ִ��
             */
        }

        /// <summary>
        /// ����Map���ض�urlִ���м��
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Test4(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Map("/abc", abcBuilder =>
            {
                abcBuilder.Use(async (context, next) =>
                {
                    await next();
                    await context.Response.WriteAsync("����Test4");
                });
            });
            /**
             * ֻ��url·��Ϊabc�Ż�ִ�е�ǰ�м��
             */
        }

        /// <summary>
        /// ����MapWhen���ض�urlִ���м��
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Test5(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.MapWhen(context =>
            {
                return context.Request.Query.Keys.Contains("abc");
            },
            builder =>
            {
                builder.Run(async context =>
                {
                    await context.Response.WriteAsync("����Test5");
                });
            });
            /**
             * �������ΪMap��Ӹ��ӵ�����ɸѡ����url��������abcʱ����뵱ǰ�м��
             * IApplicationBuilder.Run��User�������ǣ�Use�Ὣnext�ĺ����м��ע�룬���Կ���ִ�к������м����
             * ��Runֻע����context��HttpContext�������һ���м����ĩ�ˣ�������ִ�к������м��
             */
        }

        /// <summary>
        /// �Լ�дһ���м��
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Test6(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMyMiddleware();
        }
    }

    public class MyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MyMiddleware> _logger;

        public MyMiddleware(RequestDelegate next, ILogger<MyMiddleware> logger)
        {
            this._next = next;
            this._logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using (_logger.BeginScope("TraceId:{id}", context.TraceIdentifier))
            {
                _logger.LogInformation("��ʼִ��");
                await _next(context);
                _logger.LogInformation("ִ�н���");
            }
        }
    }

    public static class MyBuilderExtension
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<MyMiddleware>();
        }
    }
}
