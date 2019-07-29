using NHibernate;

namespace SIGLO.Infra.Transations
{
    public class Uow : IUow
    {
        private readonly ISessionFactory _sessionFactory;
        private ISession _session;

        public ISession Session => _session;

        public Uow(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
            _session = null;
        }

        public ISession OpenSession()
        {
            if (_session == null)
            {
                _session = _sessionFactory.OpenSession();
                _session.FlushMode = FlushMode.Commit;
                _session.CacheMode = CacheMode.Normal; //aqui voce configura algumas coisas da sessao (uma por request por ser injetada via scoped)
            }
            return _session;
        }
    }
}
