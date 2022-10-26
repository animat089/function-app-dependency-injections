using Microsoft.Extensions.Logging;
using Sample.FunctionApp.Services.Contracts;

namespace Sample.FunctionApp.Services;

/// <summary>
/// Class to represent singleton object 
/// </summary>
internal class SingletonService : BaseService, ISingletonService
{
    /// <summary>
    /// Creates an instance of <see cref="SingletonService">
    /// </summary>
    /// <param name="logger">Object of the logger service</param>
    public SingletonService(ILogger<IBaseService> logger) : base(logger)
    {
    }
}