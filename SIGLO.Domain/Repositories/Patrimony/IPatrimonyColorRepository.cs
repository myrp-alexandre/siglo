using SIGLO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SIGLO.Domain.Repositories
{
    public interface IPatrimonyColorRepository
    {
        Task Create(PatrimonyColor patrimonyColor);
        Task Update(PatrimonyColor patrimonyColor);
        Task Delete(PatrimonyColor patrimonyColor);

        Task<PatrimonyColor> GetById(long id);
        Task<List<PatrimonyColor>> GetList(int page, int pageSize);
        Task<PatrimonyColor> GetBy(Expression<Func<PatrimonyColor, bool>> expression);
        Task<List<PatrimonyColor>> GetListBy(Expression<Func<PatrimonyColor, bool>> expression, int page, int pageSize);
    }
}
