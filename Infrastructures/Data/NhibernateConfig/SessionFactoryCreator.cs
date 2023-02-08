using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Infrastructure.DAL.NhibernateConfig;
using NHibernate;
namespace Api.Infrastructures.Data.NhibernateConfig;

public class SessionFactoryCreator
{
    public static ISessionFactory Create(
        string connectionString,
        Assembly mappingAssembly)
    {
        return Fluently.Configure()
            .Database(
                SQLiteConfiguration.Standard
                    .UsingFile("ReplyRequest.db")
            )
            .Mappings(m =>
                m.FluentMappings.AddFromAssembly(mappingAssembly))
            .BuildSessionFactory();
    }


}