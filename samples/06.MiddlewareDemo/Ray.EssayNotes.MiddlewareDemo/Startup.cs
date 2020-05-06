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

            //打印下中间件链路
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
            Console.WriteLine($"当前中间件：{jsonStr}");
        }

        /// <summary>
        /// 添加中间件，且阻断
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Test1(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("测试Test1");
            });

            /**
             * 这里设置http请求返回值
             * 不管输入什么api，都会返回这个结果
             * 因为没有对执行后续中间件，所以也不会执行后续操作
             */
        }

        /// <summary>
        /// 添加中间件，且执行后续中间件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Test2(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("测试Test2");
                await next();
            });

            /**
             * 这里会报异常，因为后续中间件对Header有操作
             */
        }
        /// <summary>
        /// 判断header
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Test3(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.HasStarted)
                    await context.Response.WriteAsync("测试Test3");
            });

            /**
             * 如果header没有返回，才执行
             */
        }

        /// <summary>
        /// 利用Map对特定url执行中间件
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
                    await context.Response.WriteAsync("测试Test4");
                });
            });
            /**
             * 只有url路径为abc才会执行当前中间件
             */
        }

        /// <summary>
        /// 利用MapWhen对特定url执行中间件
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
                    await context.Response.WriteAsync("测试Test5");
                });
            });
            /**
             * 这里可以为Map添加复杂的条件筛选，当url参数含有abc时会进入当前中间件
             * IApplicationBuilder.Run与User的区别是，Use会将next的后续中间件注入，所以可以执行后续的中间件；
             * 而Run只注入了context的HttpContext，是最后一个中间件的末端，即不会执行后续的中间件
             */
        }

        /// <summary>
        /// 自己写一个中间件
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
                _logger.LogInformation("开始执行");
                await _next(context);
                _logger.LogInformation("执行结束");
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
