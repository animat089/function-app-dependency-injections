using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

var connectionString = configuration["QueueConnectionString"].ToString();
var queueName = configuration["QueueName"].ToString();

var clientOptions = new ServiceBusClientOptions() { TransportType = ServiceBusTransportType.AmqpTcp };
var client = new ServiceBusClient(connectionString, clientOptions);
var sender = client.CreateSender(queueName);

Console.WriteLine("Started");
try
{
    Parallel.For(1, 51, new ParallelOptions() { MaxDegreeOfParallelism = 8 }, i =>
    {
        var msg = new ServiceBusMessage(i.ToString());
        sender.SendMessageAsync(msg).GetAwaiter().GetResult();
    });
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    sender.DisposeAsync().GetAwaiter().GetResult();
    client.DisposeAsync().GetAwaiter().GetResult();
}

Console.WriteLine("Completed");