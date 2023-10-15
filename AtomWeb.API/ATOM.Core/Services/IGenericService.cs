using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ATOM.Core.Services
{
    public interface IGenericService<T> where T : class
    {
        Task AddAsync(T t);

        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> GetListByFilter(Expression<Func<T, bool>> expression);

        Task<T> GetByIdAsync(int id);

        void Remove(T t);

        void Update(T entity);
    }
}
