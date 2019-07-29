using NHibernate;
using NHibernate.Linq;
using SIGLO.Domain.Entities;
using SIGLO.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SIGLO.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ISession _session;

        public UserRepository(ISession session)
        {
            _session = session;
        }

        public async Task Create(User user)
        {
            await _session.SaveAsync(user);
            await _session.FlushAsync();
        }

        public async Task Update(User user)
        {
            await _session.UpdateAsync(user);
            await _session.FlushAsync();
        }

        public async Task Delete(User user)
        {
            await _session.DeleteAsync(user);
            await _session.FlushAsync();
        }

        public async Task<List<User>> GetListBy(Expression<Func<User, bool>> expression, int page, int pageSize)
        {
            return await _session.Query<User>().Skip(page).Take(pageSize).Where(expression).ToListAsync();
        }

        public async Task<List<User>> GetList(int page, int pageSize)
        {
            return await _session.Query<User>().Skip(page).Take(pageSize).ToListAsync(); ;
        }

        public async Task<User> GetById(long id)
        {
            _session.CacheMode = CacheMode.Normal;
            return await _session.GetAsync<User>(id);
        }

        public Task<User> GetBy(Expression<Func<User, bool>> expression)
        {
            return _session.Query<User>().Where(expression).AsQueryable().FirstOrDefaultAsync();
        }
    }
}