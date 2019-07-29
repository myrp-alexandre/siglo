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
    public class ContractRepository : IContractRepository
    {
        private readonly ISession _session;

        public ContractRepository(ISession session)
        {
            _session = session;
        }

        public async Task Create(Contract contract)
        {
            await _session.SaveAsync(contract);
            await _session.FlushAsync();
        }

        public async Task Update(Contract contract)
        {
            await _session.UpdateAsync(contract);
            await _session.FlushAsync();
        }

        public async Task Delete(Contract contract)
        {
            await _session.DeleteAsync(contract);
            await _session.FlushAsync();
        }

        public async Task<List<Contract>> GetListBy(Expression<Func<Contract, bool>> expression, int page, int pageSize)
        {
            return await _session.Query<Contract>().Skip(page).Take(pageSize).Where(expression).ToListAsync();
        }

        public async Task<List<Contract>> GetList(int page, int pageSize)
        {
            return await _session.Query<Contract>().Skip(page).Take(pageSize).ToListAsync(); ;
        }

        public async Task<Contract> GetById(long id)
        {
            _session.CacheMode = CacheMode.Normal;
            return await _session.GetAsync<Contract>(id);
        }

        public Task<Contract> GetBy(Expression<Func<Contract, bool>> expression)
        {
            return _session.Query<Contract>().Where(expression).AsQueryable().FirstOrDefaultAsync();
        }
    }
}