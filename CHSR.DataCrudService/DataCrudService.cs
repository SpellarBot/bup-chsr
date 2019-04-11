
using CHSR.Repository;
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

        public async Task<TEntity> Insert(TEntity entity)
        {
            await Repository.Insert(entity);
            return null;
        }
    }
}
