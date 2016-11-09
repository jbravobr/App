using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace IcatuzinhoApp
{
    public interface IBaseRepository<T>
    {
        bool Insert(T entity);

        bool Insert(List<T> entities);

        bool Delete(T entity);

        bool Update(T entity);

        bool Any();

        List<T> GetAll(Expression<Func<T, bool>> predicate);

        List<T> GetAll();

        T Get(Expression<Func<T, bool>> predicate);

        T Get();

        T GetById(int pkId);
    }
}

