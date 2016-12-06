using System;
using Xamarin.Forms;
using SQLiteNetExtensions.Extensions;
using System.Collections.Generic;
using System.Linq.Expressions;
using SQLite.Net;
using System.Linq;
using Microsoft.Practices.Unity;

namespace IcatuzinhoApp
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        readonly SQLiteConnection conn;
        object _lock = new object();

        readonly ILogExceptionService _log;

        public BaseRepository()
        {
            if (_log == null)
                _log = App._container.Resolve<ILogExceptionService>();

            conn = DependencyService.Get<ISQLite>().GetConnection();
            CreateDataBase();
        }

        /// <summary>
        /// Criação da base
        /// </summary>
        void CreateDataBase()
        {
            try
            {
                lock (_lock)
                {
                    conn.CreateTable<AuthenticationCode>();
                    conn.CreateTable<Driver>();
                    conn.CreateTable<LogException>();
                    conn.CreateTable<Schedule>();
                    conn.CreateTable<Station>();
                    conn.CreateTable<Travel>();
                    conn.CreateTable<User>();
                    conn.CreateTable<Vehicle>();
                    conn.CreateTable<Weather>();
                    conn.CreateTable<Itinerary>();
                    conn.CreateTable<AuthenticationToken>();
                }
            }
            catch (Exception ex)
            {
                _log.SubmitToInsights(ex);
            }
        }

        /// <summary>
        /// Insere ou atualiza uma entidade existente e seus filhos.
        /// </summary>
        public bool InsertOrReplaceWithChildren(T entity)
        {
            lock (_lock)
            {
                try
                {
                    conn.InsertOrReplaceWithChildren(entity, recursive: true);
                    return true;
                }
                catch (Exception ex)
                {
                    _log.SubmitToInsights(ex);
                    return false;
                }
            }
        }

        /// <summary>
        /// Insere uma nova coleção da entidade e seus filhos T no Banco de Dados.
        /// </summary>
        public bool InsertOrReplaceAllWithChildren(List<T> list)
        {
            try
            {
                lock (_lock)
                {
                    conn.InsertOrReplaceAllWithChildren(list, recursive: true);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _log.SubmitToInsights(ex);
                return false;
            }
        }

        /// <summary>
        /// Delete uma determinada entidade T do Banco de Dados
        /// </summary>
        public bool Delete(T entity)
        {
            try
            {
                lock (_lock)
                {
                    conn.Delete(entity, recursive: false);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _log.SubmitToInsights(ex);
                return false;

            }
        }

        /// <summary>
        /// Delete toda determinada entidade T do Banco de Dados
        /// </summary>
        public bool DeleteAll(List<T> entities)
        {
            try
            {
                lock (_lock)
                {

                    foreach (var entity in entities)
                    {
                        conn.Delete(entity, recursive: false);
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                _log.SubmitToInsights(ex);
                return false;

            }
        }

        /// <summary>
        /// Retorna uma coleção de Entidades T e seus filhos de acordo com um predicado
        /// </summary>
        public List<T> GetAllWithChildren(Expression<Func<T, bool>> predicate)
        {
            try
            {
                lock (_lock)
                    return conn.GetAllWithChildren<T>(predicate, recursive: true);
            }
            catch (Exception ex)
            {
                _log.SubmitToInsights(ex);
                return null;
            }
        }

        /// <summary>
        /// Retorna uma Entidade T e seus filhos de acordo com um predicado
        /// </summary>
        public T GetWithChildren(Expression<Func<T, bool>> predicate)
        {
            try
            {
                lock (_lock)
                    return conn.GetWithChildren<T>(predicate);

            }
            catch (InvalidOperationException)
            {
                return null;
            }
            catch (Exception ex)
            {
                _log.SubmitToInsights(ex);
                return null;
            }
        }

        /// <summary>
        /// Retorna T
        /// </summary>
        public T Get()
        {
            try
            {
                lock (_lock)
                    return conn.Table<T>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                _log.SubmitToInsights(ex);
                return null;
            }
        }

        /// <summary>
        /// Retorna uma Lista Entidade T sem filtros e sem filhos
        /// </summary>
        /// <returns>The all .</returns>
        public List<T> GetAll()
        {
            try
            {
                lock (_lock)
                    return conn.Table<T>().ToList();

            }
            catch (InvalidOperationException)
            {
                return null;
            }
            catch (Exception ex)
            {
                _log.SubmitToInsights(ex);
                return null;
            }
        }

        /// <summary>
        /// Retorna um único registro da entidade T com filhos pela chave primária
        /// </summary>
        /// <param name = "pkId">Chave primária para busca</param>
        public T GetWithChildrenById(int pkId)
        {
            try
            {
                lock (_lock)
                    return conn.GetWithChildren<T>(pkId, recursive: true);
            }
            catch (Exception ex)
            {
                _log.SubmitToInsights(ex);
                return null;
            }
        }

        /// <summary>
        /// Retorna todas as Entidades T e seus filhos
        /// </summary>
        public List<T> GetAllWithChildren()
        {
            try
            {
                lock (_lock)
                    return conn.GetAllWithChildren<T>(null, recursive: true);
            }
            catch (Exception ex)
            {
                _log.SubmitToInsights(ex);
                return null;
            }
        }

        /// <summary>
        /// Atualizar uma Entidade T
        /// </summary>
        public bool UpdateWithChildren(T entity)
        {
            try
            {
                lock (_lock)
                {
                    conn.UpdateWithChildren(entity);
                    return true;

                }
            }
            catch (Exception ex)
            {
                _log.SubmitToInsights(ex);
                return false;
            }
        }

        /// <summary>
        /// Verificar se existe registro
        /// </summary>
        public bool Any()
        {
            try
            {
                lock (_lock)
                    return conn.Table<T>().Count() > 0;

            }
            catch (Exception ex)
            {
                _log.SubmitToInsights(ex);
                return false;
            }
        }
    }
}

