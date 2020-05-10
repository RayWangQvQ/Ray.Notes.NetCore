using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Ray.EssayNotes.MediatorDemo
{
    class Program
    {
        public static IServiceProvider ServiceProviderRoot { get; private set; }

        async static Task Main(string[] args)
        {
            //注册
            InitContainer();

            var mediator = ServiceProviderRoot.GetService<IMediator>();


            Console.ReadLine();
        }

        private static async Task Test1()
        {
            var mediator = ServiceProviderRoot.GetService<IMediator>();
            await mediator.Send(new MyCommand { CommandName = "cmd01" });

        }

        private static async Task Test2()
        {
            var mediator = ServiceProviderRoot.GetService<IMediator>();
            await mediator.Publish(new MyEvent { EventName = "event01" });
        }

        /// <summary>
        /// 初始化容器
        /// </summary>
        private static void InitContainer()
        {
            var services = new ServiceCollection();
            services.AddMediatR(typeof(Program).Assembly);
            ServiceProviderRoot = services.BuildServiceProvider();
        }
    }
}
