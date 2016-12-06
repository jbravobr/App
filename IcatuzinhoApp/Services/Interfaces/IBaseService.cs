using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace IcatuzinhoApp
{
    public interface IBaseService<T> where T : class
    {
        bool InsertOrReplaceWithChildren(T entity);
        bool InsertOrReplaceAllWithChildren(List<T> list);
        bool Delete(T entity);
        List<T> GetAllWithChildren(Expression<Func<T, bool>> predicate);
        T GetWithChildren(Expression<Func<T, bool>> predicate);
        List<T> GetAll();
        T Get();
        T GetWithChildrenById(int pkId);
        List<T> GetAllWithChildren();
        bool UpdateWithChildren(T entity);
        bool Any();
    }
}

