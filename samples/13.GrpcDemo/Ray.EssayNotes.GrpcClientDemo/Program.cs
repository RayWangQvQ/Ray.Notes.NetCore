using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Ray.EssayNotes.GrpcServerDemo;

namespace Ray.EssayNotes.GrpcClientDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // The port number(5001) must match the port of the gRPC server.
            using GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(
                              new HelloRequest { Name = "GreeterClient" });
            Console.WriteLine("Greeting: " + reply.Message);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static async Task GetResult()
        {

        }
    }
}
