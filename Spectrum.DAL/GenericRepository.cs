using System;
using System.Linq;
using System.Linq.Expressions;
using Spectrum.DAL.RepositoryInterfaces;
using System.Data.Entity;

namespace Spectrum.DAL
{
    public abstract class GenericRepository<T> : IGenericRepository<T>
    where T : class
    {
        public enum ActionType
        {
            Add,
            Save,
            Delete
        }

        public GenericRepository()
        {
            _entities = ContextFactory.CreateContext();
         //   _entities1 = ContextFactory.CreateContext1();
        }

        private SpectrumEntities _entities;
       //private SpectrumObjectContext _entities1;

        public SpectrumEntities Context
        {
            get { return _entities; }
            set { _entities = value; }
        }
        //public SpectrumObjectContext SpectrumContext
        //{
        //    get { return _entities1; }
        //    set { _entities1 = value; }
        //}

        public virtual bool Save(T entity)
        {
            Context.Set<T>().Add(entity);
           
            Context.SaveChanges();
            return true;
        }

        public virtual bool Update(T entity)
        {
            Context.Set<T>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
           
            Context.SaveChanges();
            return true;
        }

        public virtual bool DeleteByID(Expression<Func<T, bool>> predicate)
        {
            T entity = _entities.Set<T>().Where(predicate).FirstOrDefault();

            Context.Set<T>().Remove(entity);
            
            Context.SaveChanges();
            return true;
        }

        public virtual IQueryable<T> GetAll()
        {
            IQueryable<T> query = Context.Set<T>();
            return query;
        }

        public T GetByID(Expression<Func<T, bool>> predicate)
        {
            T query = _entities.Set<T>().Where(predicate).FirstOrDefault();
            return query;
        }

        /// <summary>
        ///  Update Autonumber to GLNoRangeObjects for  this obectid and site
        /// </summary>
        /// <param name="siteCode"></param>
        /// <param name="objectID"></param>
        public void UpdateNextID(string siteCode, string objectID)
        {
            try
            {
                var glObject = Context.GLNoRangeObjects.Where(g => g.SiteCode == siteCode && g.ObjectID == objectID).FirstOrDefault();

                if (glObject != null)       // added by vipin on 23.02.2017 Mantis 600
                {
                    glObject.CurrentNo += 1;
                    Context.Entry(glObject).Property(p => p.CurrentNo).IsModified = true;  // added by vipin on 12-04-2017
                    Context.Entry(glObject).Property(p => p.CREATEDBY).IsModified = true;
                    Context.Entry(glObject).Property(p => p.CREATEDON).IsModified = true;
                    Context.Entry(glObject).Property(p => p.CREATEDON).IsModified = true;
                    Context.Entry(glObject).Property(p => p.UPDATEDBY).IsModified = true;
                  //  Context.Entry<GLNoRangeObjects>(glObject).State = EntityState.Modified;
                    Context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
              //  throw ex;
            }
        }
    }
}
