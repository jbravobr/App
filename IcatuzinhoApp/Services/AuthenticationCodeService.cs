using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IcatuzinhoApp
{
    public class AuthenticationCodeService : IAuthenticationCodeService
    {
        readonly IAuthenticationCodeRepository _repo;

        public AuthenticationCodeService(IAuthenticationCodeRepository repo)
        {
            _repo = repo;
        }

        public bool Any()
        {
            return _repo.Any();
        }

        public bool Delete(AuthenticationCode entity)
        {
            return _repo.Delete(entity);
        }

        public AuthenticationCode Get()
        {
            return _repo.Get();
        }

        public AuthenticationCode Get(Expression<Func<AuthenticationCode, bool>> predicate)
        {
            return _repo.Get(predicate);
        }

        public List<AuthenticationCode> GetAll()
        {
            return _repo.GetAll();
        }

        public List<AuthenticationCode> GetAll(Expression<Func<AuthenticationCode, bool>> predicate)
        {
            return _repo.GetAll(predicate);
        }

        public AuthenticationCode GetById(int pkId)
        {
            return _repo.GetById(pkId);
        }

        public bool Insert(AuthenticationCode entity)
        {
            return _repo.Insert(entity);
        }

        public bool Insert(List<AuthenticationCode> entities)
        {
            return _repo.Insert(entities);
        }

        public bool Update(AuthenticationCode entity)
        {
            return _repo.Update(entity);
        }
    }
}

