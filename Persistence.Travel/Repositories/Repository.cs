using Application.Travel.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Travel.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Travel.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly TravelContext _travelContext;
        public Repository(TravelContext travelContext)
        {
            _travelContext = travelContext;

        }
        public T GetList(Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
            {
                return _travelContext.Set<T>().FirstOrDefault(filter);
            }
            else
            {
                return _travelContext.Set<T>().FirstOrDefault();
            }
        }
        public void Delete(T t)
        {
            _travelContext.Set<T>().Remove(t);

           
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _travelContext.Set<T>().FindAsync(id).AsTask();
        }

        public async Task<List<T>> GetListAsync()
        {
            return await _travelContext.Set<T>().AsNoTracking().ToListAsync();
        }
        public async Task<T> FindAsync(object id)
        {
            return await _travelContext.Set<T>().FindAsync(id);

        }
        public async Task AddAsync(T t)
        {
            await _travelContext.Set<T>().AddAsync(t);
       
        }

        public void Update(T t, T unchanged)
        {
            _travelContext.Entry(unchanged).CurrentValues.SetValues(t);
   

        }

        public IQueryable<T> GetQuery()
        {
            return _travelContext.Set<T>().AsQueryable();
        }
    }
}