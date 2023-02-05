using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.EntityLayer;

namespace UpSchool.HelpDesk.DataAccessLayer.Abstract
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByFilterAsync(Expression<Func<T, bool>> filter);

        Task<T?> FindAsync(Expression<Func<T, bool>> filter);

        Task<T> CreateAsync(T entity);

        void Remove(T entity);

        IQueryable<T> GetQueryable();
    }
}
