using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace IcatuzinhoApp
{
    public interface IBaseService<T> where T : class
    {
        Task InsertOrReplaceWithChildren(T entity);
        Task InsertOrReplaceAllWithChildren(List<T> list);
        Task Delete(T entity);

        Task<List<T>> GetAllWithChildren(Expression<Func<T, bool>> predicate, string optionalRoute = "");
        Task<List<T>> GetAll(string optionalRoute = "");
        Task<List<T>> GetAllWithChildren(string optionalRoute = "");

        Task<T> GetWithChildren(Expression<Func<T, bool>> predicate, string optionalRoute = "");
        Task<T> Get(string optionalRoute = "");
        Task<T> GetWithChildrenById(int pkId, string optionalRoute = "");

        Task UpdateWithChildren(T entity);
        Task<bool> Any();

        #region Travel Specific Operations

        Task<bool> DoCheckin(int scheduleId, int userId);
        Task<bool> DoCheckout(int scheduleId, int userId);
		Task<Travel> GetTravelByScheduleId(int scheduleId);

		#endregion


		#region Schedule Specific Operations

		Task<T> GetNextSchedule();

		#endregion

        #region Login Specific Operations

        Task<bool> Login(string email, string password);

		Task<User> GetUserByLogin(string login, string password);

        #endregion
    }
}

