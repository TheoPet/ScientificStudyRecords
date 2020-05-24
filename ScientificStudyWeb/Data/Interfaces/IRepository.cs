using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ScientificStudyWeb.Data.Interfaces
{
    public interface IRepository<TEntity> where TEntity:class
    {
        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        bool Remove(TEntity entity);
      
        bool Remove(int id);

        Task<TEntity> Get(int id);

        Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> GetAll();
        
        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate);

    }
}