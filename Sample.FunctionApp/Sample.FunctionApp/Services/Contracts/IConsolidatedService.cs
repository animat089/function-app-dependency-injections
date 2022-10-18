using Microsoft.Azure.WebJobs;

namespace Sample.FunctionApp.Services.Contracts;

/// <summary>
/// Interface for the consoliadted service execution.
/// </summary>
public interface IConsolidatedService
{
    void Execute(ExecutionContext executionContext);
}