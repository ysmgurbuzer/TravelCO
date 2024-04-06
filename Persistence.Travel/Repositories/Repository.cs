using Application.Travel.Interfaces;
using Application.Travel.Services;
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
        public List<T> GetList(Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
            {
                return _travelContext.Set<T>().Where(filter).ToList();
            }
            else
            {
                return _travelContext.Set<T>().ToList();
            }
        }

        public T GetByFilter(Expression<Func<T, bool>> filter = null)
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
        public async Task Delete(T t)
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

     
        public async Task<T> FindAsync(int id)
        {
            return await _travelContext.Set<T>().FindAsync(id);

        }
        public async Task<T> AddAsync(T t)
        {
           await _travelContext.Set<T>().AddAsync(t);
          
            return t;

        }

        public async Task Update(T t, T unchanged)
        {
           _travelContext.Entry(unchanged).CurrentValues.SetValues(t);
   

        }

        public IQueryable<T> GetQuery()
        {
            return _travelContext.Set<T>().AsQueryable();
        }
    }
}