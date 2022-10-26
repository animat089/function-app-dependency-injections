using Microsoft.Extensions.Logging;
using Sample.FunctionApp.Services.Contracts;

namespace Sample.FunctionApp.Services;

internal class ScopedService : BaseService, IScopedService
{
    /// <summary>
    /// Creates an instance of <see cref="ScopedService">
    /// </summary>
    /// <param name="logger">Object of the logger service</param>
    public ScopedService(ILogger<IBaseService> logger) : base(logger)
    {
    }
}