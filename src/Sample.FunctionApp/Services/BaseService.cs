using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Sample.FunctionApp.Services.Contracts;
using System;

namespace Sample.FunctionApp.Services;

internal abstract class BaseService : IBaseService
{
    protected Guid Identifier { get; }
    protected ILogger Logger { get; }
    protected string ServiceName { get; }

    protected BaseService(ILogger<IBaseService> logger)
    {
        this.Identifier = Guid.NewGuid();
        this.Logger = logger;
        this.ServiceName = this.GetType().Name;

        this.Logger.LogInformation("Service Created:{0}||Id:{1}", this.ServiceName, this.Identifier);
    }

    protected virtual void LogWorkTrace(ExecutionContext executionContext, string message)
    {
        var finalMessage = "Function:{0}||InvocationId:{1}||Service:{2}-{3}||Id:{4}";
        this.Logger.LogTrace(finalMessage, executionContext.FunctionName, executionContext.InvocationId, this.ServiceName, message, this.Identifier);
    }

    /// <inheritdoc/>
    public abstract void DoWork(ExecutionContext executionContext, string messag);
}