using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IcatuzinhoApp
{
    public interface IBaseService<T> 
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

