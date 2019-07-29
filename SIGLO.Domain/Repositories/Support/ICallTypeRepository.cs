using SIGLO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SIGLO.Domain.Repositories
{
    public interface ICallTypeRepository
    {
        Task Create(CallType callType);
        Task Update(CallType callType);
        Task Delete(CallType callType);

        Task<CallType> GetById(long id);
        Task<List<CallType>> GetList(int page, int pageSize);
        Task<CallType> GetBy(Expression<Func<CallType, bool>> expression);
        Task<List<CallType>> GetListBy(Expression<Func<CallType, bool>> expression, int page, int pageSize);
    }
}
