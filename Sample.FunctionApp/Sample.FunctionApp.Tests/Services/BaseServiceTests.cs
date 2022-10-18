using Microsoft.Extensions.Logging;
using Moq;
using Sample.FunctionApp.Services;
using Sample.FunctionApp.Services.Contracts;

namespace Sample.FunctionApp.Tests.Services;

public abstract class BaseServiceTests
{
    protected readonly Mock<ILogger<IBaseService>> MockLogger;

    private readonly Microsoft.Azure.WebJobs.ExecutionContext executionContext;

    protected IBaseService Service { get; set; }

    protected BaseServiceTests()
    {
        MockLogger = new Mock<ILogger<IBaseService>>();
        executionContext = new Microsoft.Azure.WebJobs.ExecutionContext()
        {
            InvocationId = Guid.NewGuid(),
            FunctionName = "TestFunction"
        };
    }

    [Fact]
    public void ShouldCreateAnObject()
    {
        var service = this.Service as BaseService;
        Assert.NotNull(service);
        MockLogger.Verify(logger => logger.Log(
        It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
        It.Is<EventId>(eventId => eventId.Id == 0),
        It.Is<It.IsAnyType>((@object, @type) => @object.ToString().Length > 0),
        It.IsAny<Exception>(),
        It.IsAny<Func<It.IsAnyType, Exception, string>>()),
        Times.Once);
    }

    [Fact]
    public void ShouldDoWork()
    {
        Service.DoWork(executionContext, "TestFunction");
        MockLogger.Verify(logger => logger.Log(
        It.Is<LogLevel>(logLevel => logLevel == LogLevel.Trace),
        It.Is<EventId>(eventId => eventId.Id == 0),
        It.Is<It.IsAnyType>((@object, @type) => @object.ToString().Length > 0),
        It.IsAny<Exception>(),
        It.IsAny<Func<It.IsAnyType, Exception, string>>()),
        Times.Once);
    }
}