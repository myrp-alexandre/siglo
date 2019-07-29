using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;
using SIGLO.Infra.Maps;
using SIGLO.Shared;
using System;
using System.Collections.Generic;

namespace SIGLO.Infra
{
    public static class NHibernateSessionFactoryProvider
    {
        public static FluentConfiguration CreateFluentConfiguration()
        {
            try
            {
                Configuration cfg = new Configuration();
                Dictionary<string, string> props = new Dictionary<string, string>
                {
                    {
                        "connection.driver_class", "NHibernate.Driver.Sql2008ClientDriver" //tem que ver se e esse mesmo
                    },
                    {
                        "dialect", "NHibernate.Dialect.MsSql2008Dialect"
                    },
                    {
                        //Mostra SQL gerado pelo NHiberante no Console.Out
                        "show_sql", "true"
                    },
                    {
                        //Timeout dos Comandos
                        "command_timeout", "120"
                    },
                    {
                        //Substituições das queries HQL
                        "query.substitutions", "true 1, false 0, yes 'Y', no 'N'"
                    },
                    {
                        //É uma verificação do NH que valida se os métodos estão marcados como virtual
                        "use_proxy_validator", "false"
                    },
                    {
                        "adonet.batch_size", "50"
                    }
                    /*,
                    {
                        //utilizado para o nhibernate profiller
                        "generate_statistics", "true"
                    }*/
                };
                cfg.SetProperties(props);

                //seta a connection string
                cfg.Properties["connection.connection_string"] = AppSettings.ConnectionString;

                //configura mapeamentos
                var fluent = Fluently.Configure(cfg)
                                     .Mappings(m => m.FluentMappings.AddFromAssemblyOf<AccountMap>());

                //session context dependendendo do tipo
                //utilizacao do async_local: http://nhibernate.info/doc/nhibernate-reference/architecture.html#architecture-current-session
                fluent.ExposeConfiguration(c =>
                {
                    c.Properties.Add("current_session_context_class", "async_local");
                });

                return fluent;
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel criar a configuração de sessionfactory", ex);
            }
        }

        public static ISessionFactory CreateSessionFactory()
        {
            try
            {
                var config = CreateFluentConfiguration();
                return config.BuildSessionFactory();
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel criar a construtor de sessão.", ex);
            }
        }
    }
}
