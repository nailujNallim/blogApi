using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using NHibernate;
using zmg.blogEngine.repository;
using zmg.blogEngine.repository.mappings;

namespace zmg.blogEngine.api
{
    public class NHibernateHelper : INHibernateHelper
    {
        private readonly string _connectionString;
        private readonly string _nhProperty;
        private readonly object _lockObject = new object();
        private ISessionFactory _sessionFactory;

        public NHibernateHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _nhProperty = configuration.GetSection("NHibernate")["autoCreate"];
        }

        private ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    CreateSessionFactory();
                }

                return _sessionFactory;
            }
        }

        public ISession GetSession()
        {
            return SessionFactory.OpenSession();
        }

        private void CreateSessionFactory()
        {
            lock (_lockObject)
            {
                var fluentConfiguration = Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2012.ConnectionString(_connectionString))
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMap>())
                                  .ExposeConfiguration(
                                    cfg => cfg.SetProperty("hbm2ddl.keywords", "keywords")
                                                .SetProperty("hbm2ddl.auto", _nhProperty));

                _sessionFactory = fluentConfiguration.BuildSessionFactory();
            }
        }
    }
}
