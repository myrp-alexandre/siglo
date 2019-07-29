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
    public class CallRepository : ICallRepository
    {
        private readonly ISession _session;

        public CallRepository(ISession session)
        {
            _session = session;
        }

        public async Task Create(Call call)
        {
            await _session.SaveAsync(call);
            await _session.FlushAsync();
        }

        public async Task Update(Call call)
        {
            await _session.UpdateAsync(call);
            await _session.FlushAsync();
        }

        public async Task Delete(Call call)
        {
            await _session.DeleteAsync(call);
            await _session.FlushAsync();
        }

        public async Task<List<Call>> GetListBy(Expression<Func<Call, bool>> expression, int page, int pageSize)
        {
            return await _session.Query<Call>().Skip(page).Take(pageSize).Where(expression).ToListAsync();
        }

        public async Task<List<Call>> GetList(int page, int pageSize)
        {
            return await _session.Query<Call>().Skip(page).Take(pageSize).ToListAsync(); ;
        }

        public async Task<Call> GetById(long id)
        {
            _session.CacheMode = CacheMode.Normal;
            return await _session.GetAsync<Call>(id);
        }

        public Task<Call> GetBy(Expression<Func<Call, bool>> expression)
        {
            return _session.Query<Call>().Where(expression).AsQueryable().FirstOrDefaultAsync();
        }
    }
}