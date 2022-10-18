using Sample.FunctionApp.Services;

namespace Sample.FunctionApp.Tests.Services;

public class ScopedServiceTests : BaseServiceTests
{
    public ScopedServiceTests() : base()
    {
        Service = new ScopedService(MockLogger.Object);
    }
}