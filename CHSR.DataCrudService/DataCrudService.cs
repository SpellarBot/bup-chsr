
using CHSR.Repository;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CHSR.DataCrudService
{
    public abstract class DataCrudService<TEntity> where TEntity : class
    {
        protected Repository<TEntity> Repository { get; set; }

        public DataCrudService(Repository<TEntity> repository)
        {
            Repository = repository;
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Repository.Get();
        }

        public TEntity GetById(params object[] id)
        {
            return Repository.GetById(id);
        }

        public async Task<TEntity> GetByIdAsync(params object[] id)
        {
            return await Repository.GetByIdAsync(id);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter = null)
        {
            return Repository.FirstOrDefault();
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return Repository.FirstOrDefaultAsync();
        }

        public void Insert(TEntity entity)
        {
            Repository.Insert(entity);
        }

        public TEntity InsertandReturn(TEntity entity)
        {
            return Repository.InsertandReturn(entity);
        }

        public void Update(TEntity entityToUpdate)
        {
            Repository.Update(entityToUpdate);
        }

        public void Delete(params object[] id)
        {
            Repository.Delete(id);
        }

        public void Delete(TEntity entityToDelete)
        {
            Repository.Delete(entityToDelete);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await Repository.SaveChangesAsync();

        }
    }
}
