using SIGLO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SIGLO.Domain.Repositories
{
    public interface ICallRepository
    {
        Task Create(Call call);
        Task Update(Call call);
        Task Delete(Call call);

        Task<Call> GetById(long id);
        Task<List<Call>> GetList(int page, int pageSize);
        Task<Call> GetBy(Expression<Func<Call, bool>> expression);
        Task<List<Call>> GetListBy(Expression<Func<Call, bool>> expression, int page, int pageSize);
    }
}
