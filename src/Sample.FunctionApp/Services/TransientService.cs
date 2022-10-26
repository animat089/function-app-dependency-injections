using Microsoft.Extensions.Logging;
using Sample.FunctionApp.Services.Contracts;

namespace Sample.FunctionApp.Services;

/// <summary>
/// Class to represent transient object 
/// </summary>
internal class TransientService : BaseService, ITransientService
{
    /// <summary>
    /// Creates an instance of <see cref="TransientService">
    /// </summary>
    /// <param name="logger">Object of the logger service</param>
    public TransientService(ILogger<IBaseService> logger) : base(logger)
    {
    }
}