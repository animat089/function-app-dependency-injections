using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Sample.FunctionApp.MsgBlaster;

public class Program
{
    [ExcludeFromCodeCoverage]
    public static void Main()
    {
        // Create Configuration
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();

        // Read from User Secrets
        var connectionString = configuration["QueueConnectionString"].ToString();
        var queueName = configuration["QueueName"].ToString();

        // Create client and connections
        var clientOptions = new ServiceBusClientOptions()
        {
            TransportType = ServiceBusTransportType.AmqpTcp
        };
        var client = new ServiceBusClient(connectionString);
        var sender = client.CreateSender(queueName);

        // Send messages and exit
        try
        {
            Console.WriteLine("Started");
            Parallel.For(0, 2000, new ParallelOptions() { MaxDegreeOfParallelism = 8 }, i =>
            {
                var msg = new ServiceBusMessage(Convert.ToString(i));
                sender.SendMessageAsync(msg).GetAwaiter().GetResult();
            });
            Console.WriteLine("Completed");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        finally
        {
            sender.DisposeAsync().GetAwaiter().GetResult();
            client.DisposeAsync().GetAwaiter().GetResult();
        }
    }
}