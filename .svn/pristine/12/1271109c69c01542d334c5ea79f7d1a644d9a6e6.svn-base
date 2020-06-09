using System;
using System.Linq;
using System.Linq.Expressions;

namespace Spectrum.DAL.RepositoryInterfaces
{
    public interface IGenericRepository<T> where T : class
    {
        bool Save(T entity);

        bool Update(T entity);

        bool DeleteByID(Expression<Func<T, bool>> predicate);

        IQueryable<T> GetAll();

        T GetByID(Expression<Func<T, bool>> predicate);

        void UpdateNextID(string siteCode, string objectID);
    }
}
