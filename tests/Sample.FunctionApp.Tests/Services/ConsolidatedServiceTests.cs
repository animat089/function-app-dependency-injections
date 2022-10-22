using Moq;
using Sample.FunctionApp.Services;
using Sample.FunctionApp.Services.Contracts;

namespace Sample.FunctionApp.Tests.Services;

public class ConsolidatedServiceTests
{
    private readonly Mock<IScopedService> mockedScopedService;
    private readonly Mock<ISingletonService> mockedSingletonService;
    private readonly Mock<ITransientService> mockedTransientService;
    private readonly Mock<Microsoft.Azure.WebJobs.ExecutionContext> mockedExecutionContext;

    private readonly IConsolidatedService consolidatedService;

    public ConsolidatedServiceTests()
    {
        mockedScopedService = new Mock<IScopedService>();
        mockedScopedService.Setup(x => x.DoWork(It.IsAny<Microsoft.Azure.WebJobs.ExecutionContext>(), "ConsolidatedService"));

        mockedSingletonService = new Mock<ISingletonService>();
        mockedSingletonService.Setup(x => x.DoWork(It.IsAny<Microsoft.Azure.WebJobs.ExecutionContext>(), "ConsolidatedService"));

        mockedTransientService = new Mock<ITransientService>();
        mockedTransientService.Setup(x => x.DoWork(It.IsAny<Microsoft.Azure.WebJobs.ExecutionContext>(), "ConsolidatedService"));

        mockedExecutionContext = new Mock<Microsoft.Azure.WebJobs.ExecutionContext>();
        consolidatedService = new ConsolidatedService(mockedSingletonService.Object, mockedScopedService.Object, mockedTransientService.Object);
    }

    [Fact]
    public void ShouldCreateAnObject()
    {
        Assert.NotNull(consolidatedService);
    }

    [Fact]
    public void ShouldWxecuteWhnCalled()
    {
        consolidatedService.Execute(mockedExecutionContext.Object);

        mockedScopedService.Verify(x => x.DoWork(It.IsAny<Microsoft.Azure.WebJobs.ExecutionContext>(), "ConsolidatedService"));
        mockedSingletonService.Verify(x => x.DoWork(It.IsAny<Microsoft.Azure.WebJobs.ExecutionContext>(), "ConsolidatedService"));
        mockedTransientService.Verify(x => x.DoWork(It.IsAny<Microsoft.Azure.WebJobs.ExecutionContext>(), "ConsolidatedService"));
    }
}