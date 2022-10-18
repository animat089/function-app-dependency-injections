using Sample.FunctionApp.Services;

namespace Sample.FunctionApp.Tests.Services;

public class SingletonServiceTests : BaseServiceTests
{
    public SingletonServiceTests() : base()
    {
        Service = new SingletonService(MockLogger.Object);
    }
}