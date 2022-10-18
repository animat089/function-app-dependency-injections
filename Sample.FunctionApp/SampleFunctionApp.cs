using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Sample.FunctionApp.Services.Contracts;

namespace Sample.FunctionApp;

public class SampleFunctionApp
{
    private readonly IScopedService scopedService;
    private readonly ISingletonService singletonService;
    private readonly ITransientService transientService;
    private readonly IConsolidatedService consolidatedService;
    private readonly ILogger logger;

    public SampleFunctionApp(ISingletonService singletonService, IScopedService scopedService, ITransientService transientService, IConsolidatedService consolidatedService, ILogger<SampleFunctionApp> logger)
    {
        this.scopedService = scopedService;
        this.singletonService = singletonService;
        this.transientService = transientService;
        this.consolidatedService = consolidatedService;
        this.logger = logger;
    }

    [FunctionName("ProcessServiceBusMessage")]
    public void ProcessServiceBusMessage(
        [ServiceBusTrigger(queueName: "%QueueName%", Connection = "QueueConnectionString")] string messageContent, ExecutionContext executionContext)
    {
        scopedService.DoWork(executionContext, executionContext.FunctionName);
        singletonService.DoWork(executionContext, executionContext.FunctionName);
        transientService.DoWork(executionContext, executionContext.FunctionName);
        consolidatedService.Execute(executionContext);
        logger.LogTrace("Function:{0}||InvocationId:{1}||Message:{2}", executionContext.FunctionName, executionContext.InvocationId, messageContent);
    }
}