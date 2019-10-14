using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;

namespace CardHolder.DAL.Interface
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Fetch();

        IQueryable<T> GetQuery();

        IEnumerable<T> GetAll();

        IEnumerable<T> Find(Func<T, bool> where);

        T Single(Func<T, bool> where);

        T SingleOrDefault(Func<T, bool> where);

        T First(Func<T, bool> where);

        void Delete(T entity);

        void Delete(Func<T, bool> predicate);

        void Add(T entity);

        void Attach(T entity);

        void SaveChanges();

        void SaveChanges(SaveOptions options);

        void StateManager(T entity, System.Data.EntityState state);
    }
}