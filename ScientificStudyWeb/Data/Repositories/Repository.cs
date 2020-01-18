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

        public async Task<IEnumerable<TEntity>> GetAll()
        {
             return await dbSet.ToListAsync();
        }

        public async Task <IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }

        public async Task<TEntity> GetByID(int id)
        {
            return await dbSet.FindAsync(id);     
        } 
        

        public void Remove(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public void Remove(int id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            if(entityToDelete != null)
                dbSet.Remove(entityToDelete);
        }
    }
}