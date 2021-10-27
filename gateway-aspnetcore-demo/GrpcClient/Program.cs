using Grpc.Net.Client;

using GrpcService1;

using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
        
            using var channel = GrpcChannel.ForAddress("http://192.168.43.94:9080/");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(
                              new HelloRequest { Name = "GreeterClient" });
            Console.WriteLine(reply);
        }
    }
}
