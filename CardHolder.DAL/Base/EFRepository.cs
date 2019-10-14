using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using CardHolder.DAL.Interface;

namespace CardHolder.DAL.Base
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        private ObjectContext _context;
        private IObjectSet<T> _objectSet;

        protected ObjectContext Context
        {
            get
            {
                if (_context == null)
                {
                    _context = GetCurrentUnitOfWork<EFUnitOfWork>().Context;
                }

                return _context;
            }
        }

        protected IObjectSet<T> ObjectSet
        {
            get
            {
                if (_objectSet == null)
                {
                    _objectSet = this.Context.CreateObjectSet<T>();
                }

                return _objectSet;
            }
        }

        public TUnitOfWork GetCurrentUnitOfWork<TUnitOfWork>() where TUnitOfWork : IUnitOfWork
        {
            return (TUnitOfWork)UnitOfWork.Current;
        }

        public IQueryable<T> Fetch()
        {
            return ObjectSet;
        }

        public IQueryable<T> GetQuery()
        {
            return ObjectSet;
        }

        public IEnumerable<T> GetAll()
        {
            return GetQuery().ToList();
        }

        public IEnumerable<T> Find(Func<T, bool> where)
        {
            return this.ObjectSet.Where<T>(where);
        }

        public T Single(Func<T, bool> where)
        {
            return this.ObjectSet.SingleOrDefault<T>(where);
        }

        public T SingleOrDefault(Func<T, bool> where)
        {
            return this.ObjectSet.SingleOrDefault<T>(where);
        }

        public T First(Func<T, bool> where)
        {
            return this.ObjectSet.First<T>(where);
        }

        public virtual void Delete(T entity)
        {
            this.ObjectSet.DeleteObject(entity);
        }

        public virtual void Delete(Func<T, bool> predicate)
        {
            IEnumerable<T> records = from x in this.ObjectSet.Where<T>(predicate) select x;
            foreach (T record in records)
            {
                this.ObjectSet.DeleteObject(record);
            }
        }

        public virtual void Add(T entity)
        {
            this.ObjectSet.AddObject(entity);
        }

        public void Attach(T entity)
        {
            this.ObjectSet.Attach(entity);
        }

        public void SaveChanges()
        {
            this.Context.SaveChanges();
        }

        public void SaveChanges(SaveOptions options)
        {
            this.Context.SaveChanges(options);
        }

        public void StateManager(T Entity, System.Data.EntityState state)
        {
            this.Context.ObjectStateManager.ChangeObjectState(Entity, state);
        }
    }
}