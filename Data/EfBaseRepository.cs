
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

        public async Task<TEntity?> GetById(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }     
        
        public async Task<bool> Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);            
            return await Save();
        }
        public async Task<bool> Insert(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            return await Save();
        }
        public async Task<bool> Update(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
            return await Save();
        }
        public async Task<bool> Delete(int id)
        {
            TEntity? entity = await GetById(id);
            return entity == null ? false : await Delete(entity);
        }
        protected async Task<bool> Save()
        {
            try
            {
                await context.SaveChangesAsync();
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
