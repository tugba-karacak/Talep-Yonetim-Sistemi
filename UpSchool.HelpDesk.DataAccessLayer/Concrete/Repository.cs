using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UpSchool.HelpDesk.DataAccessLayer.Abstract;
using UpSchool.HelpDesk.DataAccessLayer.Contexts;
using UpSchool.HelpDesk.EntityLayer;

namespace UpSchool.HelpDesk.DataAccessLayer.Concrete
{
    public class Repository<T> : IRepository<T> where T : class, IEntity, new()
    {
        public readonly HelpDeskContext context;
        public Repository(HelpDeskContext context)
        {
            this.context = context;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await this.context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByFilterAsync(Expression<Func<T,bool>> filter)
        {
            return await this.context.Set<T>().AsNoTracking().SingleOrDefaultAsync(filter);
        }

        public async Task<T?> FindAsync(Expression<Func<T,bool>> filter)
        {
            return await this.context.Set<T>().SingleOrDefaultAsync(filter);
        }

        public async Task<T> CreateAsync(T entity)
        {
            await this.context.Set<T>().AddAsync(entity);
            return entity;
        }

        public void Remove(T entity)
        {
            this.context.Set<T>().Remove(entity);
        }

        public IQueryable<T> GetQueryable()
        {
            return this.context.Set<T>().AsQueryable();
        }
     }
}
