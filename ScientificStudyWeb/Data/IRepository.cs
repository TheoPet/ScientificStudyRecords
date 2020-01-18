using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ScientificStudyWeb.Data
{
    public interface IRepository<TEntity> where TEntity:class
    {
        void Add(TEntity entity);

        void Remove(TEntity entity);
      
        void Remove(int id);

        Task<TEntity> GetByID(int id);

        Task<IEnumerable<TEntity>> GetAll();
        
        Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate);

    }
}