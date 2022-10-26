using Microsoft.Azure.WebJobs;

namespace Sample.FunctionApp.Services.Contracts;

/// <summary>
/// Interface for the consoliadted service execution.
/// </summary>
public interface IConsolidatedService
{
    /// <summary>
    /// Action performed in the consolidated service.
    /// </summary>
    /// <param name="executionContext">execution context of the function.</param>
    void Execute(ExecutionContext executionContext);
}