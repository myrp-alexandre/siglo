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
    public class PatrimonyColorRepository : IPatrimonyColorRepository
    {
        private readonly ISession _session;

        public PatrimonyColorRepository(ISession session)
        {
            _session = session;
        }

        public async Task Create(PatrimonyColor patrimonyColor)
        {
            await _session.SaveAsync(patrimonyColor);
            await _session.FlushAsync();
        }

        public async Task Update(PatrimonyColor patrimonyColor)
        {
            await _session.UpdateAsync(patrimonyColor);
            await _session.FlushAsync();
        }

        public async Task Delete(PatrimonyColor patrimonyColor)
        {
            await _session.DeleteAsync(patrimonyColor);
            await _session.FlushAsync();
        }

        public async Task<List<PatrimonyColor>> GetListBy(Expression<Func<PatrimonyColor, bool>> expression, int page, int pageSize)
        {
            return await _session.Query<PatrimonyColor>().Skip(page).Take(pageSize).Where(expression).ToListAsync();
        }

        public async Task<List<PatrimonyColor>> GetList(int page, int pageSize)
        {
            return await _session.Query<PatrimonyColor>().Skip(page).Take(pageSize).ToListAsync(); ;
        }

        public async Task<PatrimonyColor> GetById(long id)
        {
            _session.CacheMode = CacheMode.Normal;
            return await _session.GetAsync<PatrimonyColor>(id);
        }

        public Task<PatrimonyColor> GetBy(Expression<Func<PatrimonyColor, bool>> expression)
        {
            return _session.Query<PatrimonyColor>().Where(expression).AsQueryable().FirstOrDefaultAsync();
        }
    }
}