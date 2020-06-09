using System;
using System.Linq;
using System.Linq.Expressions;

namespace Spectrum.BL.BusinessInterface
{
    public interface IGenericManager<T> where T : class
    {
        /// <summary> 
        /// Save entity to the repository 
        /// </summary> 
        /// <param name="entity">the entity to save</param> 
        /// <returns>The saved entity</returns> 
        bool Save(T entity);

        /// <summary> 
        /// Updates entity within the the repository 
        /// </summary> 
        /// <param name="entity">the entity to update</param> 
        /// <returns>The updates entity</returns> 
        bool Update(T entity);

        /// <summary> 
        /// Mark entity to be deleted within the repository 
        /// </summary> 
        /// <param name="entity">The entity to delete</param> 
        bool DeleteByID(Expression<Func<T, bool>> predicate);
       
        /// Get a selected extiry by the object primary key ID
        /// </summary>
        /// <param name="id">Primary key ID</param>
        T GetByID(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Get all the element of this repository
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();
    }
}
