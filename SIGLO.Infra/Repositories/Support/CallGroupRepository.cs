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
    public class CallGroupRepository : ICallGroupRepository
    {
        private readonly ISession _session;

        public CallGroupRepository(ISession session)
        {
            _session = session;
        }

        public async Task Create(CallGroup callGroup)
        {
            await _session.SaveAsync(callGroup);
            await _session.FlushAsync();
        }

        public async Task Update(CallGroup callGroup)
        {
            await _session.UpdateAsync(callGroup);
            await _session.FlushAsync();
        }

        public async Task Delete(CallGroup callGroup)
        {
            await _session.DeleteAsync(callGroup);
            await _session.FlushAsync();
        }

        public async Task<List<CallGroup>> GetListBy(Expression<Func<CallGroup, bool>> expression, int page, int pageSize)
        {
            return await _session.Query<CallGroup>().Skip(page).Take(pageSize).Where(expression).ToListAsync();
        }

        public async Task<List<CallGroup>> GetList(int page, int pageSize)
        {
            return await _session.Query<CallGroup>().Skip(page).Take(pageSize).ToListAsync(); ;
        }

        public async Task<CallGroup> GetById(long id)
        {
            _session.CacheMode = CacheMode.Normal;
            return await _session.GetAsync<CallGroup>(id);
        }

        public Task<CallGroup> GetBy(Expression<Func<CallGroup, bool>> expression)
        {
            return _session.Query<CallGroup>().Where(expression).AsQueryable().FirstOrDefaultAsync();
        }
    }
}