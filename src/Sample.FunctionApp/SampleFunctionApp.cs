using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Sample.FunctionApp.Services.Contracts;
using System.Threading;

namespace Sample.FunctionApp;

/// <summary>
/// Sample Function App
/// </summary>
public class SampleFunctionApp
{
    private readonly IScopedService scopedService;
    private readonly ISingletonService singletonService;
    private readonly ITransientService transientService;
    private readonly IConsolidatedService consolidatedService;
    private readonly ILogger logger;

    /// <summary>
    /// Creates an instance of <see cref="SampleFunctionApp" />
    /// </summary>
    /// <param name="singletonService">Object for singleton service</param>
    /// <param name="scopedService">Object for scoped service</param>
    /// <param name="transientService">Object for transient service</param>
    /// <param name="consolidatedService">Object for consolidatedService</param>
    /// <param name="logger">Object for microsoft logger</param>
    public SampleFunctionApp(ISingletonService singletonService, IScopedService scopedService, ITransientService transientService, IConsolidatedService consolidatedService, ILogger<SampleFunctionApp> logger)
    {
        this.scopedService = scopedService;
        this.singletonService = singletonService;
        this.transientService = transientService;
        this.consolidatedService = consolidatedService;
        this.logger = logger;
    }

    /// <summary>
    /// Function that processes the message on the queue
    /// </summary>
    /// <param name="messageContent">String content on the message</param>
    /// <param name="executionContext">Object of the execution context of the message</param>
    [FunctionName("ProcessServiceBusMessage")]
    public void ProcessServiceBusMessage(
        [ServiceBusTrigger(queueName: "%QueueName%", Connection = "QueueConnectionString")] string messageContent, Microsoft.Azure.WebJobs.ExecutionContext executionContext)
    {
        scopedService.DoWork(executionContext, executionContext.FunctionName);
        singletonService.DoWork(executionContext, executionContext.FunctionName);
        transientService.DoWork(executionContext, executionContext.FunctionName);
        consolidatedService.Execute(executionContext);

        logger.LogTrace("Function:{0}||InvocationId:{1}||Message:{2}", executionContext.FunctionName, executionContext.InvocationId, messageContent);
        Thread.Sleep(10000);
    }
}