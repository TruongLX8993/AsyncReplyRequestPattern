using System.Reflection;
using Api.Infrastructures.Data;
using Api.Infrastructures.Data.NhibernateConfig;
using Domain.Repository;
using Infrastructures.Data.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using ISession=NHibernate.ISession;
namespace Infrastructures;

public static class InfrastructuresConfig
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var connectionString = configuration["ConnectionString"]!;
        var sessionFactory = SessionFactoryCreator.Create(connectionString,
            Assembly.GetExecutingAssembly());
        serviceCollection.AddSingleton(sessionFactory);
        serviceCollection.AddScoped<ISession>(serviceProvide => serviceProvide.GetService<ISessionFactory>()!
            .OpenSession());
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        serviceCollection.AddScoped<IResponseRepository, ResponseRepository>();
        serviceCollection.AddScoped<IRequestRepository, RequestRepository>();
        return serviceCollection;
    }
}