using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Sample.FunctionApp.Services.Contracts;

namespace Sample.FunctionApp.Services;

internal class SingletonService : BaseService, ISingletonService
{
    public SingletonService(ILogger<IBaseService> logger) : base(logger)
    {
    }

    public override void DoWork(ExecutionContext executionContext, string message)
    {
        LogWorkTrace(executionContext, message);
    }
}