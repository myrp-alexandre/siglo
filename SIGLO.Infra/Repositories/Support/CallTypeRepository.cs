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
    public class CallTypeRepository : ICallTypeRepository
    {
        private readonly ISession _session;

        public CallTypeRepository(ISession session)
        {
            _session = session;
        }

        public async Task Create(CallType callType)
        {
            await _session.SaveAsync(callType);
            await _session.FlushAsync();
        }

        public async Task Update(CallType callType)
        {
            await _session.UpdateAsync(callType);
            await _session.FlushAsync();
        }

        public async Task Delete(CallType callType)
        {
            await _session.DeleteAsync(callType);
            await _session.FlushAsync();
        }

        public async Task<List<CallType>> GetListBy(Expression<Func<CallType, bool>> expression, int page, int pageSize)
        {
            return await _session.Query<CallType>().Skip(page).Take(pageSize).Where(expression).ToListAsync();
        }

        public async Task<List<CallType>> GetList(int page, int pageSize)
        {
            return await _session.Query<CallType>().Skip(page).Take(pageSize).ToListAsync(); ;
        }

        public async Task<CallType> GetById(long id)
        {
            _session.CacheMode = CacheMode.Normal;
            return await _session.GetAsync<CallType>(id);
        }

        public Task<CallType> GetBy(Expression<Func<CallType, bool>> expression)
        {
            return _session.Query<CallType>().Where(expression).AsQueryable().FirstOrDefaultAsync();
        }
    }
}