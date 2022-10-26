using Microsoft.Azure.WebJobs;
using Sample.FunctionApp.Services.Contracts;

namespace Sample.FunctionApp.Services;

/// <summary>
/// Consolidated service that uses all the serivces
/// </summary>
public class ConsolidatedService : IConsolidatedService
{
    private readonly IBaseService scopedService;
    private readonly IBaseService singletonService;
    private readonly IBaseService transientService;

    /// <summary>
    /// Create a new instance of the <see cref="ConsolidatedService"/>
    /// </summary>
    /// <param name="singletonService">Object for singleton service</param>
    /// <param name="scopedService">Object for scoped service</param>
    /// <param name="transientService">Object for transient service</param>
    public ConsolidatedService(ISingletonService singletonService, IScopedService scopedService, ITransientService transientService)
    {
        this.scopedService = scopedService;
        this.singletonService = singletonService;
        this.transientService = transientService;
    }

    /// <inheritdoc/>
    public void Execute(ExecutionContext executionContext)
    {
        const string message = "ConsolidatedService";

        this.scopedService.DoWork(executionContext, message);
        this.singletonService.DoWork(executionContext, message);
        this.transientService.DoWork(executionContext, message);
    }
}