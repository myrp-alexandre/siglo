using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using SIGLO.Infra.Maps;
using SIGLO.Shared;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace SIGLO.Infra.Context
{
    public sealed class SIGLODataContext_NH
    {
        private static ISessionFactory _sessionFactory;

        public static ISessionFactory SessionFactory()
        {
            if (_sessionFactory != null) return _sessionFactory;

            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.GetAssembly(typeof(AccountMap)).GetTypes());
            HbmMapping meuMapeamentoDominio = mapper.CompileMappingForAllExplicitlyAddedEntities();
            var configuracao = new Configuration();
            configuracao.DataBaseIntegration(
                c =>
                {
                    c.Dialect<MsSql2008Dialect>();
                    c.ConnectionString = AppSettings.ConnectionString;
                    //c.LogSqlInConsole = true;
                    //c.LogFormattedSql = true;
                    c.SchemaAction = SchemaAutoAction.Create;
                });
            configuracao.AddMapping(meuMapeamentoDominio);
            var SessionFactory = configuracao.BuildSessionFactory();

            return _sessionFactory;
        }

        public static ISession OpenSession()
        {
            return _sessionFactory.OpenSession();
        }
    }

    public sealed class SIGLODataContext_DP : IDisposable
    {
        public SqlConnection Conn { get; set; }
        public SIGLODataContext_DP()
        {
            Conn = new SqlConnection(AppSettings.ConnectionString);
            Conn.Open();
        }

        public void Dispose()
        {
            if (Conn.State != ConnectionState.Closed)
                Conn.Close();
        }
    }
}
