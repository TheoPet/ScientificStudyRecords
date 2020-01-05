using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ScientificStudiesRecord.Data
{
    public interface IRepository<TEntity> where TEntity:class
    {
        Task Add(TEntity entity);

        Task Remove(TEntity entity);
      
        Task Remove(int id);

        Task<TEntity> GetByID(int id);

        Task<IEnumerable<TEntity>> Get(
               Expression<Func<TEntity, bool>> filter = null,
               Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
               string includeProperties = "");

        Task Update(TEntity entity);
    }
}