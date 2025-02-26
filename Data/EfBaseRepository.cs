
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Data
{
    public class EfBaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext context;

        public EfBaseRepository(DbContext context)
        {
            this.context = context;            
        }

        public IQueryable<TEntity> GetAll()
        {           
            return context.Set<TEntity>();
        }

        public TEntity? GetById(int id)
        {
            return context.Set<TEntity>().Find(id);
        }     
        
        public bool Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);            
            return Save();
        }
        public bool Insert(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            return Save();
        }
        public bool Update(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
            return Save();
        }
        public bool Delete(int id)
        {
            TEntity? entity = GetById(id);
            return entity == null ? false : Delete(entity);
        }
        protected bool Save()
        {
            try
            {
                context.SaveChanges();
                return true;
            }
            catch (DbUpdateException ex)
            {                
                foreach (var item in ex.Entries)
                {
                    item.State = EntityState.Detached;
                }
                return false;
            }
        }
    }
}
