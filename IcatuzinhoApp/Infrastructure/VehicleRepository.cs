using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Realms;

namespace IcatuzinhoApp
{
    public class VehicleRepository : BaseRepository, IVehicleRepository
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
                return _realm.All<Vehicle>().Any();
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
        public bool Delete(Vehicle entity)
        {
            _realm = Realm.GetInstance();

            try
            {
                if (_realm.All<Vehicle>().Any(x => x.Id == entity.Id))
                {
                    using (var tran = _realm.BeginWrite())
                    {
                        _realm.Remove(_realm.All<Vehicle>().First(x => x.Id == entity.Id));
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
        public Vehicle Get()
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<Vehicle>().First();
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
        public Vehicle Get(Expression<Func<Vehicle, bool>> predicate)
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<Vehicle>().First(predicate);
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
        public List<Vehicle> GetAll()
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<Vehicle>().ToList();
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
        public List<Vehicle> GetAll(Expression<Func<Vehicle, bool>> predicate)
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<Vehicle>().Where(predicate).ToList();
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
        public Vehicle GetById(int pkId)
        {
            try
            {
                return _realm.All<Vehicle>().First(x => x.Id == pkId);
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
        public bool Insert(Vehicle entity)
        {
            _realm = Realm.GetInstance();

            using (var tran = _realm.BeginWrite())
            {
                try
                {
                    var obj = _realm.CreateObject<Vehicle>();
                    obj.Id = entity.Id;
                    obj.Number = entity.Number;
                    obj.SeatsAvailable = entity.SeatsAvailable;
                    obj.SeatsTotal = entity.SeatsTotal;

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
        public bool Insert(List<Vehicle> entities)
        {
            _realm = Realm.GetInstance();

            foreach (var entity in entities)
            {
                using (var tran = _realm.BeginWrite())
                {
                    try
                    {
                        var obj = _realm.CreateObject<Vehicle>();

                        obj.Id = entity.Id;
                        obj.Number = entity.Number;
                        obj.SeatsAvailable = entity.SeatsAvailable;
                        obj.SeatsTotal = entity.SeatsTotal;
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
        public bool Update(Vehicle entity)
        {
            _realm = Realm.GetInstance();

            try
            {
                using (var tran = _realm.BeginWrite())
                {
                    var obj = _realm.All<Vehicle>().First(x => x.Id == entity.Id);

                    obj.Id = entity.Id;
                    obj.Number = entity.Number;
                    obj.SeatsAvailable = entity.SeatsAvailable;
                    obj.SeatsTotal = entity.SeatsTotal;

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

