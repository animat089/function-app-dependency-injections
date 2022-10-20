using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();
var connectionString = configuration["QueueConnectionString"].ToString();
var queueName = configuration["QueueName"].ToString();
var clientOptions = new ServiceBusClientOptions()
{
    TransportType = ServiceBusTransportType.AmqpTcp
};

var client = new ServiceBusClient(connectionString);
var sender = client.CreateSender(queueName);

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
