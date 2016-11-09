using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IcatuzinhoApp
{
    public class VehicleService : IVehicleService
    {
        IVehicleRepository _repo;

        public VehicleService(IVehicleRepository repo)
        {
            _repo = repo;
        }

        public bool Any()
        {
            return _repo.Any();
        }

        public bool Delete(Vehicle entity)
        {
            return _repo.Delete(entity);
        }

        public Vehicle Get()
        {
            return _repo.Get();
        }

        public Vehicle Get(Expression<Func<Vehicle, bool>> predicate)
        {
            return _repo.Get(predicate);
        }

        public List<Vehicle> GetAll()
        {
            return _repo.GetAll();
        }

        public List<Vehicle> GetAll(Expression<Func<Vehicle, bool>> predicate)
        {
            return _repo.GetAll(predicate);
        }

        public Vehicle GetById(int pkId)
        {
            return _repo.GetById(pkId);
        }

        public bool Insert(List<Vehicle> entities)
        {
            return _repo.Insert(entities);
        }

        public bool Insert(Vehicle entity)
        {
            return _repo.Insert(entity);
        }

        public bool Update(Vehicle entity)
        {
            return _repo.Update(entity);
        }
    }
}

