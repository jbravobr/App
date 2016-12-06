using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Realms;

namespace IcatuzinhoApp
{
    public class WeatherRepository : BaseRepository, IWeatherRepository
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
                return _realm.All<Weather>().Any();
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
        public bool Delete(Weather entity)
        {
            _realm = Realm.GetInstance();

            try
            {
                if (_realm.All<Weather>().Any(x => x.Id == entity.Id))
                {
                    using (var tran = _realm.BeginWrite())
                    {
                        _realm.Remove(_realm.All<Weather>().First(x => x.Id == entity.Id));
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
        public Weather Get()
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<Weather>().First();
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
        public Weather Get(Expression<Func<Weather, bool>> predicate)
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<Weather>().First(predicate);
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
        public List<Weather> GetAll()
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<Weather>().ToList();
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
        public List<Weather> GetAll(Expression<Func<Weather, bool>> predicate)
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<Weather>().Where(predicate).ToList();
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
        public Weather GetById(int pkId)
        {
            try
            {
                _realm = Realm.GetInstance();
                return _realm.All<Weather>().First(x => x.Id == pkId);
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
        public bool Insert(Weather entity)
        {
            _realm = Realm.GetInstance();

            using (var tran = _realm.BeginWrite())
            {
                try
                {
                    var obj = _realm.CreateObject<Weather>();
                    obj.Ico = entity.Ico;
                    obj.Id = entity.Id;
                    obj.Temp = entity.Temp;
                    obj.WeatherDesc = entity.WeatherDesc;

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
        public bool Insert(List<Weather> entities)
        {
            _realm = Realm.GetInstance();

            foreach (var entity in entities)
            {
                using (var tran = _realm.BeginWrite())
                {
                    try
                    {
                        var obj = _realm.CreateObject<Weather>();

                        obj.Ico = entity.Ico;
                        obj.Id = entity.Id;
                        obj.Temp = entity.Temp;
                        obj.WeatherDesc = entity.WeatherDesc;

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
        public bool Update(Weather entity)
        {
            _realm = Realm.GetInstance();

            try
            {
                using (var tran = _realm.BeginWrite())
                {
                    var obj = _realm.All<Weather>().First(x => x.Id == entity.Id);

                    obj.Ico = entity.Ico;
                    obj.Id = entity.Id;
                    obj.Temp = entity.Temp;
                    obj.WeatherDesc = entity.WeatherDesc;

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

