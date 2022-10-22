using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Sample.FunctionApp.Services;
using Sample.FunctionApp.Services.Contracts;
using System.Diagnostics.CodeAnalysis;

[assembly: FunctionsStartup(typeof(Sample.FunctionApp.Startup))]

namespace Sample.FunctionApp;

[ExcludeFromCodeCoverage]
internal class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddScoped<IConsolidatedService, ConsolidatedService>();
        builder.Services.AddScoped<IScopedService, ScopedService>();
        builder.Services.AddSingleton<ISingletonService, SingletonService>();
        builder.Services.AddTransient<ITransientService, TransientService>();
    }
}