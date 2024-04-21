using ToDoService.Application.Common.Interfaces;
using ToDoService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ToDoService.Application.Contracts.Persistence;
using ToDoService.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            _ = services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("ToDoListServiceDb"));
        }
        else
        {
            _ = services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ToDoListConnectionString"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }
        _ = services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        _ = services.AddScoped<ITaskRepository, TaskRepository>();

        _ = services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        _ = services.AddScoped<ApplicationDbContextInitialiser>();

        _ = services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        return services;
    }
}
