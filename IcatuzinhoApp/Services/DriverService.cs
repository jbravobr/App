using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IcatuzinhoApp
{
    public class DriverService : IDriveService
    {
        IDriverRepository _repo;

        public DriverService(IDriverRepository repo)
        {
            _repo = repo;
        }

        public bool Any()
        {
            return _repo.Any();
        }

        public bool Delete(Driver entity)
        {
            return _repo.Delete(entity);
        }

        public Driver Get()
        {
            return _repo.Get();
        }

        public Driver Get(Expression<Func<Driver, bool>> predicate)
        {
            return _repo.Get(predicate);
        }

        public List<Driver> GetAll()
        {
            return _repo.GetAll();
        }

        public List<Driver> GetAll(Expression<Func<Driver, bool>> predicate)
        {
            return _repo.GetAll(predicate);
        }

        public Driver GetById(int pkId)
        {
            return _repo.GetById(pkId);
        }

        public bool Insert(Driver entity)
        {
            return _repo.Insert(entity);
        }

        public bool Insert(List<Driver> entities)
        {
            return _repo.Insert(entities);
        }

        public bool Update(Driver entity)
        {
            return _repo.Update(entity);
        }
    }
}

