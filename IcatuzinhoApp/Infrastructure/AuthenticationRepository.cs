using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Realms;
using System.Diagnostics;

namespace IcatuzinhoApp
{
    public class AuthenticationRepository : BaseRepository, IAuthenticationRepository
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
                return _realm.All<AuthenticationToken>().Any();
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
        public bool Delete(AuthenticationToken entity)
        {
            _realm = Realm.GetInstance();

            try
            {
                if (_realm.All<AuthenticationToken>().Any(x => x.Id == entity.Id))
                {
                    using (var tran = _realm.BeginWrite())
                    {
                        _realm.Remove(_realm.All<AuthenticationToken>().First(x => x.Id == entity.Id));
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
        public AuthenticationToken Get()
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<AuthenticationToken>().First();
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
        public AuthenticationToken Get(Expression<Func<AuthenticationToken, bool>> predicate)
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<AuthenticationToken>().First(predicate);
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
        public List<AuthenticationToken> GetAll()
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<AuthenticationToken>().ToList();
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
        public List<AuthenticationToken> GetAll(Expression<Func<AuthenticationToken, bool>> predicate)
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<AuthenticationToken>().Where(predicate).ToList();
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
        public AuthenticationToken GetById(int pkId)
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<AuthenticationToken>().First(x => x.Id == pkId);
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
        public bool Insert(AuthenticationToken entity)
        {
            _realm = Realm.GetInstance();

            using (var tran = _realm.BeginWrite())
            {
                try
                {
                    var obj = _realm.CreateObject<AuthenticationToken>();
                    obj.AccessToken = entity.AccessToken;
                    obj.Id = entity.Id;
                    obj.RefreshToken = entity.RefreshToken;

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
        public bool Insert(List<AuthenticationToken> entities)
        {
            _realm = Realm.GetInstance();

            foreach (var entity in entities)
            {
                using (var tran = _realm.BeginWrite())
                {
                    try
                    {
                        var obj = _realm.CreateObject<AuthenticationToken>();

                        obj.AccessToken = entity.AccessToken;
                        obj.Id = entity.Id;
                        obj.RefreshToken = entity.RefreshToken;
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
        public bool Update(AuthenticationToken entity)
        {
            _realm = Realm.GetInstance();

            try
            {
                using (var tran = _realm.BeginWrite())
                {
                    var obj = _realm.All<AuthenticationToken>().First(x => x.Id == entity.Id);
                    obj.AccessToken = entity.AccessToken;
                    obj.Id = entity.Id;
                    obj.RefreshToken = entity.RefreshToken;

                    tran.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log(ex);
                return false;
            }
        }
    }
}

