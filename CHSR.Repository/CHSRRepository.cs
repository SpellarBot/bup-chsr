using System;
using System.Collections.Generic;
using System.Text;

namespace CHSR.Repository
{
    public class CHSRRepository<T> : IGenericRepository<T> where T : class
    {
        //private readonly CHSRContext _context;

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            //_context.Institutes.ToListAsync()
            throw new NotImplementedException();
        }

        public T GetById(object id)
        {
            throw new NotImplementedException();
        }

        public void Insert(T obj)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
