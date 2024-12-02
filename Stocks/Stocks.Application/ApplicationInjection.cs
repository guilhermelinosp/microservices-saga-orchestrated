using Stocks.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Stocks.Application;

public static class ApplicationInjection
{
    public static async Task AddApplicationInjection(this IServiceCollection services, IConfiguration configuration)
    {
        await services.AddInfrastructureInjection(configuration);
        await Task.CompletedTask;
    }
}
