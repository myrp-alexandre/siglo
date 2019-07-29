using SIGLO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SIGLO.Domain.Repositories
{
    public interface ISectorRepository
    {
        Task Create(Sector sector);
        Task Update(Sector sector);
        Task Delete(Sector sector);

        Task<Sector> GetById(long id);
        Task<List<Sector>> GetList(int page, int pageSize);
        Task<Sector> GetBy(Expression<Func<Sector, bool>> expression);
        Task<List<Sector>> GetListBy(Expression<Func<Sector, bool>> expression, int page, int pageSize);
    }
}
