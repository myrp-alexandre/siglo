using NHibernate;
using System.Threading.Tasks;

namespace SIGLO.Infra.Transations
{
    public interface IUow
    {
        ISession OpenSession();        
    }
}
