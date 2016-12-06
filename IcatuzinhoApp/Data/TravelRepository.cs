using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Realms;
using System.Diagnostics;

namespace IcatuzinhoApp
{
    public class TravelRepository : BaseRepository, ITravelRepository
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
                return _realm.All<Travel>().Any();
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
        public bool Delete(Travel entity)
        {
            _realm = Realm.GetInstance();

            try
            {
                if (_realm.All<Travel>().Any(x => x.Id == entity.Id))
                {
                    using (var tran = _realm.BeginWrite())
                    {
                        _realm.Remove(_realm.All<Travel>().First(x => x.Id == entity.Id));
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
        public Travel Get()
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<Travel>().First();
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
        public Travel Get(Expression<Func<Travel, bool>> predicate)
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<Travel>().First(predicate);
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
        public List<Travel> GetAll()
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<Travel>().ToList();
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
        public List<Travel> GetAll(Expression<Func<Travel, bool>> predicate)
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<Travel>().Where(predicate).ToList();
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
        public Travel GetById(int pkId)
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<Travel>().First(x => x.Id == pkId);
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
        public bool Insert(Travel entity)
        {
            _realm = Realm.GetInstance();

            using (var tran = _realm.BeginWrite())
            {
                try
                {
                    var obj = _realm.CreateObject<Travel>();

                    obj.Driver = entity.Driver;
                    obj.Id = entity.Id;
                    obj.Schedule = entity.Schedule;
                    obj.Vehicle = entity.Vehicle;

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
        public bool Insert(List<Travel> entities)
        {
            _realm = Realm.GetInstance();

            foreach (var entity in entities)
            {
                using (var tran = _realm.BeginWrite())
                {
                    try
                    {
                        var obj = _realm.CreateObject<Travel>();

                        obj.Driver = entity.Driver;
                        obj.Id = entity.Id;
                        obj.Schedule = entity.Schedule;
                        obj.Vehicle = entity.Vehicle;

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
        public bool Update(Travel entity)
        {
            _realm = Realm.GetInstance();

            try
            {
                using (var tran = _realm.BeginWrite())
                {
                    var obj = _realm.All<Travel>().First(x => x.Id == entity.Id);

                    obj.Driver = entity.Driver;
                    obj.Id = entity.Id;
                    obj.Schedule = entity.Schedule;
                    obj.Vehicle = entity.Vehicle;

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

