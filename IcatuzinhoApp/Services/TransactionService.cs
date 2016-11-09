using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IcatuzinhoApp
{
    public class TransactionService : ITransactionService
    {
        ITransactionRepository _repo;

        public TransactionService(ITransactionRepository repo)
        {
            _repo = repo;
        }

        public bool Any()
        {
            return _repo.Any();
        }

        public bool Delete(Transaction entity)
        {
            return _repo.Delete(entity);
        }

        public Transaction Get()
        {
            return _repo.Get();
        }

        public Transaction Get(Expression<Func<Transaction, bool>> predicate)
        {
            return _repo.Get(predicate);
        }

        public List<Transaction> GetAll()
        {
            return _repo.GetAll();
        }

        public List<Transaction> GetAll(Expression<Func<Transaction, bool>> predicate)
        {
            return _repo.GetAll(predicate);
        }

        public Transaction GetById(int pkId)
        {
            return _repo.GetById(pkId);
        }

        public bool Insert(List<Transaction> entities)
        {
            return _repo.Insert(entities);
        }

        public bool Insert(Transaction entity)
        {
            return _repo.Insert(entity);
        }

        public bool Update(Transaction entity)
        {
            return _repo.Update(entity);
        }
    }
}

