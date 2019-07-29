using System.Threading.Tasks;

namespace SIGLO.Infra.Transations
{
    public interface IUow
    {
        Task Commit();
        void Rollback();            
    }
}
