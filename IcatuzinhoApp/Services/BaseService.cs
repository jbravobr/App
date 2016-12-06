using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IcatuzinhoApp
{
    public abstract class BaseService<T> : IBaseService<T> where T : class, new()
    {
        public BaseRepository<T> repository;

        /// <summary>
        /// Insere os novos registros quando estes são lista.
        /// </summary>
        /// <returns>The or replace all with children .</returns>
        /// <param name="list">List.</param>
        public bool InsertOrReplaceAllWithChildren(List<T> list)
        {
            InitiateRepository();
            return repository.InsertOrReplaceAllWithChildren(list);
        }

        /// <summary>
        /// Deleta o registro informado baseado na entidade.
        /// </summary>
        /// <returns>The .</returns>
        /// <param name="entidade">Entidade.</param>
        public bool Delete(T entidade)
        {
            InitiateRepository();
            return repository.Delete(entidade);
        }

        /// <summary>
        /// Retorna os dados da entidade e todos os relacionados base numa expressão filtro.
        /// </summary>
        /// <returns>The all with children .</returns>
        /// <param name="predicate">Predicate.</param>
        public List<T> GetAllWithChildren(Expression<Func<T, bool>> predicate)
        {
            if (predicate != null && predicate.Parameters.Count > 0)
            {
                InitiateRepository();
                return repository.GetAllWithChildren(predicate);
            }

            return null;
        }

        /// <summary>
        /// Retorna todos os registros em lista do predicado informado.
        /// </summary>
        /// <returns>The .</returns>
        /// <param name="predicate">Predicate.</param>
        public T Get(Expression<Func<T, bool>> predicate)
        {
            if (predicate != null && predicate.Parameters.Count > 0)
            {
                InitiateRepository();
                return repository.GetWithChildren(predicate);
            }

            return null;
        }

        /// <summary>
        /// Retorna todos os registros e seus filhos, sem filtro.
        /// </summary>
        /// <returns>The all with children .</returns>
        public List<T> GetAllWithChildren()
        {
            InitiateRepository();
            return repository.GetAllWithChildren();
        }

        /// <summary>
        /// Retornar uma  entidade com filtro em seu ID.
        /// </summary>
        /// <returns>The with children by identifier .</returns>
        /// <param name="pkId">Pk identifier.</param>
        public T GetWithChildrenById(int pkId)
        {
            if (pkId > 0)
            {
                InitiateRepository();
                return repository.GetWithChildrenById(pkId);
            }

            return null;
        }

        /// <summary>
        /// Efetua a atualiação de uma entidade.
        /// </summary>
        /// <returns>The with children .</returns>
        /// <param name="entity">Entity.</param>
        public bool UpdateWithChildren(T entity)
        {
            InitiateRepository();
            return repository.UpdateWithChildren(entity);
        }

        /// <summary>
        /// Efetua a inserção de um registro e dos seus filhos.
        /// </summary>
        /// <returns>The or replace with children .</returns>
        /// <param name="entity">Entity.</param>
        public bool InsertOrReplaceWithChildren(T entity)
        {
            InitiateRepository();
            return repository.InsertOrReplaceWithChildren(entity);
        }

        /// <summary>
        /// Cria uma instância do repositório caso não exista uma já
        /// </summary>
        public void InitiateRepository()
        {
            if (repository == null)
                repository = new BaseRepository<T>();
        }

        /// <summary>
        /// Retorna uma entidade e seus filhos.
        /// </summary>
        public T GetWithChildren(Expression<Func<T, bool>> predicate)
        {
            InitiateRepository();
            return repository.GetWithChildren(predicate);
        }

        /// <summary>
        /// Retorna uma lista de uma entidade.
        /// </summary>
        public List<T> GetAll()
        {
            InitiateRepository();
            return repository.GetAll();
        }

        /// <summary>
        /// Retorna T.
        /// </summary>
        public T Get()
        {
            InitiateRepository();
            return repository.Get();
        }

        /// <summary>
        /// Verifica se existe a entidade informada
        /// </summary>
        public bool Any()
        {
            InitiateRepository();
            return repository.Any();
        }
    }
}

