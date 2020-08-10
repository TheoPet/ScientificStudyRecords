using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScientificStudyWeb.Data.Interfaces;


namespace ScientificStudyWeb.Data
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext _context;
        internal DbSet<TEntity> dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            dbSet = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
             dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            foreach(var entity in entities)
                dbSet.Add(entity);
        }
        
        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
             return await dbSet.ToListAsync();
        }

        public virtual async Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            var result = await dbSet.Where(predicate).ToListAsync();
            return result.FirstOrDefault();
        }

        public virtual async Task <IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> Get(int id)
        {
            return await dbSet.FindAsync(id);     
        } 
        
        public virtual bool Remove(TEntity entity)
        {
            dbSet.Remove(entity);
            return true;
        }

        public virtual bool Remove(int id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            if(entityToDelete != null)
            {
                dbSet.Remove(entityToDelete);
                return true;
            }
            return false;
        }
    }
}