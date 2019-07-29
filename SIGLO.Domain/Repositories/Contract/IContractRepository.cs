using SIGLO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SIGLO.Domain.Repositories
{
    public interface IContractRepository
    {
        Task Create(Contract contract);
        Task Update(Contract contract);
        Task Delete(Contract contract);

        Task<Contract> GetById(long id);
        Task<List<Contract>> GetList(int page, int pageSize);
        Task<Contract> GetBy(Expression<Func<Contract, bool>> expression);
        Task<List<Contract>> GetListBy(Expression<Func<Contract, bool>> expression, int page, int pageSize);
    }
}
