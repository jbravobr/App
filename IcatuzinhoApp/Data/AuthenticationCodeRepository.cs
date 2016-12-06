using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Realms;

namespace IcatuzinhoApp
{
    public class AuthenticationCodeRepository : BaseRepository, IAuthenticationCodeRepository
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
                return _realm.All<AuthenticationCode>().Any();
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
        public bool Delete(AuthenticationCode entity)
        {
            _realm = Realm.GetInstance();

            try
            {
                if (_realm.All<AuthenticationCode>().Any(x => x.Id == entity.Id))
                {
                    using (var tran = _realm.BeginWrite())
                    {
                        _realm.Remove(_realm.All<AuthenticationCode>().First(x => x.Id == entity.Id));
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
        public AuthenticationCode Get()
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<AuthenticationCode>().First();
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
        public AuthenticationCode Get(Expression<Func<AuthenticationCode, bool>> predicate)
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<AuthenticationCode>().First(predicate);
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
        public List<AuthenticationCode> GetAll()
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<AuthenticationCode>().ToList();
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
        public List<AuthenticationCode> GetAll(Expression<Func<AuthenticationCode, bool>> predicate)
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<AuthenticationCode>().Where(predicate).ToList();
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
        public AuthenticationCode GetById(int pkId)
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<AuthenticationCode>().First(x => x.Id == pkId);
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
        public bool Insert(AuthenticationCode entity)
        {
            _realm = Realm.GetInstance();

            using (var tran = _realm.BeginWrite())
            {
                try
                {
                    var obj = _realm.CreateObject<AuthenticationCode>();

                    obj.Code = entity.Code;
                    obj.Id = entity.Id;
                    obj.User = entity.User;

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
        public bool Insert(List<AuthenticationCode> entities)
        {
            _realm = Realm.GetInstance();

            foreach (var entity in entities)
            {
                using (var tran = _realm.BeginWrite())
                {
                    try
                    {
                        var obj = _realm.CreateObject<AuthenticationCode>();

                        obj.Code = entity.Code;
                        obj.Id = entity.Id;
                        obj.User = entity.User;

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
        public bool Update(AuthenticationCode entity)
        {
            _realm = Realm.GetInstance();

            try
            {
                using (var tran = _realm.BeginWrite())
                {
                    var obj = _realm.All<AuthenticationCode>().First(x => x.Id == entity.Id);

                    obj.Code = entity.Code;
                    obj.Id = entity.Id;
                    obj.User = entity.User;

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

