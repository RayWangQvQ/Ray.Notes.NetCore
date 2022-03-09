using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ray.EssayNotes.ExceptionDemo.Exceptions;

namespace Ray.EssayNotes.ExceptionDemo
{
    public class Startup03
    {
        public Startup03(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(jsonOptions =>
                {
                    jsonOptions.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;//����json���л���������
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    //��http�����л�ȡException�쳣����
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    Exception ex = exceptionHandlerPathFeature?.Error;

                    var knownException = ex as IKnownException;

                    if (knownException != null)//ʵ����IKnownException�ӿڣ����������Զ�����쳣
                    {
                        knownException = KnownException.Build(knownException);
                        context.Response.StatusCode = StatusCodes.Status200OK;
                    }
                    else//δʵ��IKnownException�ӿڣ������������Զ������֪�쳣
                    {
                        var logger = context.RequestServices.GetService<ILogger<Startup03>>();
                        logger.LogError(exceptionHandlerPathFeature?.Error, exceptionHandlerPathFeature?.Error.Message);

                        knownException = KnownException.Unknown;
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    }

                    var jsonOptions = context.RequestServices.GetService<IOptions<JsonOptions>>();
                    context.Response.ContentType = "application/json; charset=utf-8";
                    await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(knownException, jsonOptions.Value.JsonSerializerOptions));
                });
            });

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
