using Microsoft.Azure.WebJobs;
using Sample.FunctionApp.Services.Contracts;

namespace Sample.FunctionApp.Services;

public class ConsolidatedService : IConsolidatedService
{
    private readonly IBaseService scopedService;
    private readonly IBaseService singletonService;
    private readonly IBaseService transientService;

    public ConsolidatedService(ISingletonService singletonService, IScopedService scopedService, ITransientService transientService)
    {
        this.scopedService = scopedService;
        this.singletonService = singletonService;
        this.transientService = transientService;
    }

    public void Execute(ExecutionContext executionContext)
    {
        var message = "ConsolidatedService";

        this.scopedService.DoWork(executionContext, message);
        this.singletonService.DoWork(executionContext, message);
        this.transientService.DoWork(executionContext, message);
    }
}