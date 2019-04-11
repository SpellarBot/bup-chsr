using CHSR.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CHSR.Repository
{
    public class Repository<TEntity> where TEntity : class
    {
        protected ApplicationDbContext Context { get; set; }        
        public Repository(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<bool> Insert(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
            var isEntitySaved = await Context.SaveChangesAsync();
            return isEntitySaved == 1 ? true : false;
        }
    }
}
