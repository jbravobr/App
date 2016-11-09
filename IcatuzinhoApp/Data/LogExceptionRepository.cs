using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Realms;

namespace IcatuzinhoApp
{
    public class LogExceptionRepository : BaseRepository, ILogExceptionRepository
    {
        private Realm _realm;

        /// <summary>
        /// Retorna uma instância da entidade principal.
        /// </summary>
        public bool Any()
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<LogException>().Any();
            }
            catch (Exception ex)
            {
                Log(ex);
                return false;
            }

        }

        /// <summary>
        /// Deleta uma instância da entidade principal.
        /// </summary>
        /// <param name="entity">Entity.</param>
        public bool Delete(LogException entity)
        {
            _realm = Realm.GetInstance();

            try
            {
                if (_realm.All<LogException>().Any(x => x.Id == entity.Id))
                {
                    using (var tran = _realm.BeginWrite())
                    {
                        _realm.Remove(_realm.All<LogException>().First(x => x.Id == entity.Id));
                        tran.Commit();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Log(ex);
                return false;
            }

        }

        /// <summary>
        /// Retorna uma instância da entidade principal.
        /// </summary>
        public LogException Get()
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<LogException>().First();
            }
            catch (Exception ex)
            {
                Log(ex);
                return null;
            }

        }

        /// <summary>
        /// Retorna uma instância da entidade principal usando um filtro (predicado).
        /// </summary>
        /// <param name="predicate">Filtro</param>
        public LogException Get(Expression<Func<LogException, bool>> predicate)
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<LogException>().First(predicate);
            }
            catch (Exception ex)
            {
                Log(ex);
                return null;
            }

        }

        /// <summary>
        /// Retorna todas as instâncias da entidade principal.
        /// </summary>
        public List<LogException> GetAll()
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<LogException>().ToList();
            }
            catch (Exception ex)
            {
                Log(ex);
                return null;
            }

        }

        /// <summary>
        /// Retorna todas as intâncias da entidade principal usando um filtro (predicado).
        /// </summary>
        /// <param name="predicate">Predicado</param>
        public List<LogException> GetAll(Expression<Func<LogException, bool>> predicate)
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<LogException>().Where(predicate).ToList();
            }
            catch (Exception ex)
            {
                Log(ex);
                return null;
            }

        }

        /// <summary>
        /// Retorna uma instância da entidade principal buscando só pelo ID.
        /// </summary>
        public LogException GetById(int pkId)
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<LogException>().First(x => x.Id == pkId);
            }
            catch (Exception ex)
            {
                Log(ex);
                return null;
            }

        }

        /// <summary>
        /// Insere um novo registro na entidade principal.
        /// </summary>
        public bool Insert(LogException entity)
        {
            _realm = Realm.GetInstance();

            using (var tran = _realm.BeginWrite())
            {
                try
                {
                    var obj = _realm.CreateObject<LogException>();

                    obj.DtCreation = entity.DtCreation;
                    obj.Exception = entity.Exception;
                    obj.Id = entity.Id;
                    obj.InnerException = entity.InnerException;
                    obj.Trasaction = entity.Trasaction;

                    tran.Commit();
                    return true;
                }
                catch (RealmException rEx)
                {
                    Log(rEx);
                    return false;
                }
                catch (Exception ex)
                {
                    Log(ex);
                    return false;
                }
            }
        }

        /// <summary>
        /// Insere uma lista de novos registros na entidade principal.
        /// </summary>
        public bool Insert(List<LogException> entities)
        {
            _realm = Realm.GetInstance();

            foreach (var entity in entities)
            {
                using (var tran = _realm.BeginWrite())
                {
                    try
                    {
                        var obj = _realm.CreateObject<LogException>();

                        obj.DtCreation = entity.DtCreation;
                        obj.Exception = entity.Exception;
                        obj.Id = entity.Id;
                        obj.InnerException = entity.InnerException;
                        obj.Trasaction = entity.Trasaction;
                        tran.Commit();

                    }
                    catch (RealmException rEx)
                    {
                        Log(rEx);
                        return false;
                    }
                    catch (Exception ex)
                    {
                        Log(ex);
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Atualizar uma instância da entidade principal.
        /// </summary>
        public bool Update(LogException entity)
        {
            _realm = Realm.GetInstance();

            try
            {
                using (var tran = _realm.BeginWrite())
                {
                    var obj = _realm.CreateObject<LogException>();

                    obj.DtCreation = entity.DtCreation;
                    obj.Exception = entity.Exception;
                    obj.Id = entity.Id;
                    obj.InnerException = entity.InnerException;
                    obj.Trasaction = entity.Trasaction;

                    tran.Commit();
                }
                return true;
            }
            catch (Exception ex)
            {
                Log(ex);
                return false;
            }
        }
    }
}

