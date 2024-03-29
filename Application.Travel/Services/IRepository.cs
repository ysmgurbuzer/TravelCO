
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Interfaces
{
    public interface IRepository<T>where T : class
    {

        T GetByFilter(Expression<Func<T, bool>> filter = null);
        Task Delete(T t);
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetListAsync();
        Task<T> AddAsync(T t);
        Task Update(T t, T unchanged);

        Task<T> FindAsync(int id);
        IQueryable<T> GetQuery();

        List<T> GetList(Expression<Func<T, bool>> filter = null);
    }
}
