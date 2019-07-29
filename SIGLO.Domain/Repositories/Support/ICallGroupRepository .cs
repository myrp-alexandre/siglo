using SIGLO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SIGLO.Domain.Repositories
{
    public interface ICallGroupRepository
    {
        Task Create(CallGroup callGroup);
        Task Update(CallGroup callGroup);
        Task Delete(CallGroup callGroup);

        Task<CallGroup> GetById(long id);
        Task<List<CallGroup>> GetList(int page, int pageSize);
        Task<CallGroup> GetBy(Expression<Func<CallGroup, bool>> expression);
        Task<List<CallGroup>> GetListBy(Expression<Func<CallGroup, bool>> expression, int page, int pageSize);
    }
}
