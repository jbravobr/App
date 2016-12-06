using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Realms;

namespace IcatuzinhoApp
{
    public class StationRepository : BaseRepository, IStationRepository
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
                return _realm.All<Station>().Any();
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
        public bool Delete(Station entity)
        {
            _realm = Realm.GetInstance();

            try
            {
                if (_realm.All<Station>().Any(x => x.Id == entity.Id))
                {
                    using (var tran = _realm.BeginWrite())
                    {
                        _realm.Remove(_realm.All<Station>().First(x => x.Id == entity.Id));
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
        public Station Get()
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<Station>().First();
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
        public Station Get(Expression<Func<Station, bool>> predicate)
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<Station>().First(predicate);
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
        public List<Station> GetAll()
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<Station>().ToList();
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
        public List<Station> GetAll(Expression<Func<Station, bool>> predicate)
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<Station>().Where(predicate).ToList();
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
        public Station GetById(int pkId)
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<Station>().First(x => x.Id == pkId);
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
        public bool Insert(Station entity)
        {
            _realm = Realm.GetInstance();

            using (var tran = _realm.BeginWrite())
            {
                try
                {
                    var obj = _realm.CreateObject<Station>();

                    obj.Id = entity.Id;
                    obj.Latitude = entity.Latitude;
                    obj.Longitude = entity.Longitude;
                    obj.Name = entity.Name;
                    obj.Order = entity.Order;

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
        public bool Insert(List<Station> entities)
        {
            _realm = Realm.GetInstance();

            foreach (var entity in entities)
            {
                using (var tran = _realm.BeginWrite())
                {
                    try
                    {
                        var obj = _realm.CreateObject<Station>();

                        obj.Id = entity.Id;
                        obj.Latitude = entity.Latitude;
                        obj.Longitude = entity.Longitude;
                        obj.Name = entity.Name;
                        obj.Order = entity.Order;
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
        public bool Update(Station entity)
        {
            _realm = Realm.GetInstance();

            try
            {
                using (var tran = _realm.BeginWrite())
                {
                    var obj = _realm.CreateObject<Station>();

                    obj.Id = entity.Id;
                    obj.Latitude = entity.Latitude;
                    obj.Longitude = entity.Longitude;
                    obj.Name = entity.Name;
                    obj.Order = entity.Order;

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

