using SIGLO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SIGLO.Domain.Repositories
{
    public interface IUserRepository
    {
        Task Create(User user);
        Task Update(User user);
        Task Delete(User user);

        Task<User> GetById(long id);
        Task<List<User>> GetList(int page, int pageSize);
        Task<User> GetBy(Expression<Func<User, bool>> expression);
        Task<List<User>> GetListBy(Expression<Func<User, bool>> expression, int page, int pageSize);
    }
}
