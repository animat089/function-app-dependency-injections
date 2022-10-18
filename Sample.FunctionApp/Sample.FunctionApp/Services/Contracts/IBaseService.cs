using Microsoft.Azure.WebJobs;

namespace Sample.FunctionApp.Services.Contracts;

/// <summary>
/// Interface for the service execution.
/// </summary>
public interface IBaseService
{
    /// <summary>
    /// Action performed in the service.
    /// </summary>
    /// <param name="executionContext">execution context of the function.</param>
    /// <param name="message">message to be passed, if required.</param>
    void DoWork(ExecutionContext executionContext, string message = null);
}