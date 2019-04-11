using CHSR.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CHSR.Repository
{
    public class Repository<TEntity> where TEntity : class
    {
        protected ApplicationDbContext Context { get; set; }   
        protected DbSet<TEntity> DbSet { get; set; }

        public Repository(ApplicationDbContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public virtual void Delete(params object[] id)
        {
            TEntity entityToDelete = DbSet.Find(id);
            Update(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            Update(entityToDelete);
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter != null ? DbSet.FirstOrDefault(filter) : DbSet.FirstOrDefault();
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter == null)
                return await DbSet.FirstOrDefaultAsync();
            else
                return await DbSet.FirstOrDefaultAsync(filter);
        }
        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }

        public virtual TEntity GetById(params object[] id)
        {
            return DbSet.Find(id);
        }

        public virtual async Task<TEntity> GetByIdAsync(params object[] id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual TEntity InsertandReturn(TEntity entity)
        {
            return DbSet.Add(entity).Entity;
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            if (Context.Entry(entityToUpdate).State == EntityState.Detached)
            {
                DbSet.Attach(entityToUpdate);
            }
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public async virtual Task<bool> SaveChangesAsync()
        {

            var result = await Context.SaveChangesAsync();
            return result == 1 ? true : false;
        }
    }
}
