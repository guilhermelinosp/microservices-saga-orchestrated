using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Orchestrators.Infrastructure;

public static class InfrastructureInjection
{
    public static async Task AddInfrastructureInjection(this IServiceCollection services, IConfiguration configuration)
    {
        // Adicione aqui os serviços relacionados à infraestrutura, como repositórios, DBContexts, etc.
        await Task.CompletedTask;
    }
}