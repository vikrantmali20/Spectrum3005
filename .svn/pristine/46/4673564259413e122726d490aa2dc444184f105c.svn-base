using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spectrum.BL.BusinessInterface;
using Spectrum.DAL.RepositoryInterfaces;
using System.Linq.Expressions;

namespace Spectrum.BL
{
    public abstract class GenericManager<T> : IGenericManager<T>
    where T : class
    {
        private IGenericRepository<T> repository;
        public GenericManager()
        {
        }
        public GenericManager(IGenericRepository<T> _repository)
        {
            this.repository = _repository;
        }
       
        public bool Save(T entity)
        {
            return this.repository.Save(entity);
        }

        public bool Update(T entity)
        {
            return this.repository.Update(entity);
        }

        public bool DeleteByID(Expression<Func<T, bool>> predicate)
        {
            return this.DeleteByID(predicate);
        }

        public T GetByID(Expression<Func<T, bool>> predicate)
        {
            return this.repository.GetByID(predicate);
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> query = this.repository.GetAll();

            return query;
        }
        
    }
}
