using FullStackCleanArchitecture.Web.Managers.Employee;
namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddServicesManagers(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeManger, EmployeeManger>();
        return services;
    }
}
