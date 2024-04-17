using ToDoService.Application.Common.Interfaces;
using ToDoService.Infrastructure.Persistence;
using ToDoService.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ToDoService.Infrastructure.Services;
using ToDoService.Application.Contracts.Persistence;
using ToDoService.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.AddScoped<AuditableEntitySaveChangesInterceptor>();

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

        _ = services.AddTransient<IDateTime, DateTimeService>();

        return services;
    }
}
