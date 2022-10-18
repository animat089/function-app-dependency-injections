using Microsoft.Extensions.Logging;
using Moq;
using Sample.FunctionApp.Services.Contracts;

namespace Sample.FunctionApp.Tests;

public class SampleFunctionAppTests
{
    private readonly Mock<ILogger<SampleFunctionApp>> mockedLogger;
    private readonly Mock<IScopedService> mockedScopedService;
    private readonly Mock<ISingletonService> mockedSingletonService;
    private readonly Mock<ITransientService> mockedTransientService;
    private readonly Mock<IConsolidatedService> mockedConsolidatedService;

    private readonly SampleFunctionApp sampleFunctionApp;

    public SampleFunctionAppTests()
    {
        mockedLogger = new Mock<ILogger<SampleFunctionApp>>();

        mockedScopedService = new Mock<IScopedService>();
        mockedScopedService.Setup(x => x.DoWork(It.IsAny<Microsoft.Azure.WebJobs.ExecutionContext>(), "ProcessServiceBusMessage"));

        mockedSingletonService = new Mock<ISingletonService>();
        mockedSingletonService.Setup(x => x.DoWork(It.IsAny<Microsoft.Azure.WebJobs.ExecutionContext>(), "ProcessServiceBusMessage"));

        mockedTransientService = new Mock<ITransientService>();
        mockedTransientService.Setup(x => x.DoWork(It.IsAny<Microsoft.Azure.WebJobs.ExecutionContext>(), "ProcessServiceBusMessage"));

        mockedConsolidatedService = new Mock<IConsolidatedService>();
        mockedConsolidatedService.Setup(x => x.Execute(It.IsAny<Microsoft.Azure.WebJobs.ExecutionContext>()));

        sampleFunctionApp = new SampleFunctionApp(mockedSingletonService.Object, mockedScopedService.Object, mockedTransientService.Object, mockedConsolidatedService.Object, mockedLogger.Object);
    }

    [Fact]
    public void ShouldCreateObject()
    {
        Assert.NotNull(sampleFunctionApp);
    }

    [Fact]
    public void ShouldProcessMessage()
    {
        var executionContext = new Microsoft.Azure.WebJobs.ExecutionContext()
        {
            InvocationId = Guid.NewGuid(),
            FunctionName = "TestFunction"
        };

        sampleFunctionApp.ProcessServiceBusMessage("TestMessage", executionContext);

        mockedScopedService.Verify(x => x.DoWork(It.IsAny<Microsoft.Azure.WebJobs.ExecutionContext>(), "TestFunction"), Times.Once);
        mockedSingletonService.Verify(x => x.DoWork(It.IsAny<Microsoft.Azure.WebJobs.ExecutionContext>(), "TestFunction"), Times.Once);
        mockedTransientService.Verify(x => x.DoWork(It.IsAny<Microsoft.Azure.WebJobs.ExecutionContext>(), "TestFunction"), Times.Once);
        mockedConsolidatedService.Verify(x => x.Execute(It.IsAny<Microsoft.Azure.WebJobs.ExecutionContext>()), Times.Once);
        mockedLogger.Verify(logger => logger.Log(
        It.Is<LogLevel>(logLevel => logLevel == LogLevel.Trace),
        It.Is<EventId>(eventId => eventId.Id == 0),
        It.Is<It.IsAnyType>((@object, @type) => @object.ToString().Length > 0),
        It.IsAny<Exception>(),
        It.IsAny<Func<It.IsAnyType, Exception, string>>()),
        Times.Once);
    }
}