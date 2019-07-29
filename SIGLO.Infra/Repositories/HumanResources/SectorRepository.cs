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
    public class SectorRepository : ISectorRepository
    {
        private readonly ISession _session;

        public SectorRepository(ISession session)
        {
            _session = session;
        }

        public async Task Create(Sector sector)
        {
            await _session.SaveAsync(sector);
            await _session.FlushAsync();
        }

        public async Task Update(Sector sector)
        {
            await _session.UpdateAsync(sector);
            await _session.FlushAsync();
        }

        public async Task Delete(Sector sector)
        {
            await _session.DeleteAsync(sector);
            await _session.FlushAsync();
        }

        public async Task<List<Sector>> GetListBy(Expression<Func<Sector, bool>> expression, int page, int pageSize)
        {
            return await _session.Query<Sector>().Skip(page).Take(pageSize).Where(expression).ToListAsync();
        }

        public async Task<List<Sector>> GetList(int page, int pageSize)
        {
            return await _session.Query<Sector>().Skip(page).Take(pageSize).ToListAsync(); ;
        }

        public async Task<Sector> GetById(long id)
        {
            _session.CacheMode = CacheMode.Normal;
            return await _session.GetAsync<Sector>(id);
        }

        public Task<Sector> GetBy(Expression<Func<Sector, bool>> expression)
        {
            return _session.Query<Sector>().Where(expression).AsQueryable().FirstOrDefaultAsync();
        }
    }
}