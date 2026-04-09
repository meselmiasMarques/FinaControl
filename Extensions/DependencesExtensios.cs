using FinaControl.Repositories;
using FinaControl.Repositories.Abstractions;
using FinaControl.Services;

namespace FinaControl.Extensios;

public static class DependencesExtensios
{
    public static void AddRepositories(this IServiceCollection services)
    {
        
        services.AddTransient<TransactionRepository>();
        services.AddTransient<UserRepository>();
        services.AddTransient<ICategoryRepository,CategoryRepository>();
        services.AddTransient<IRoleRepository,RoleRepository>();
        services.AddTransient<IUserRepository,UserRepository>();
    }

    public static void AddUnitOfWork(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();
    }
    
    public static void AddServices(this IServiceCollection services)
    {
        services.AddTransient<TokenService>();
        services.AddTransient<ICategoryService, CategoryService> ();
        services.AddTransient<IUserService, UserService> ();
        services.AddTransient<IRoleService, RoleService> ();
    }
    

}