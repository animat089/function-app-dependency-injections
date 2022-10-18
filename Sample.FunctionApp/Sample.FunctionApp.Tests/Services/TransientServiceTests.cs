using Sample.FunctionApp.Services;

namespace Sample.FunctionApp.Tests.Services;

public class TransientServiceTests : BaseServiceTests
{
    public TransientServiceTests() : base()
    {
        Service = new TransientService(MockLogger.Object);
    }
}