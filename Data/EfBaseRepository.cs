
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
            // 'Set' as a noun = DbSet
            // class CarRepostiory : EfBaseRepository<Car>
            return context.Set<TEntity>();
        }

        public TEntity? GetById(int id)
        {
            return context.Set<TEntity>().Find(id);
        }

        /*
         * Unit of work ~ Business operation ~ Transaction management
         * SQL .. DML transactions: BEGIN TRAN ... COMMIT/ROLLBACK
         * Logic .. Unit of Work transaction .. SaveChanges()
         *      IRepository.SaveChanges()
         * Microservice .. Saga design pattern
         */
        
        public bool Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
            // context.SaveChanges();
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
                // ex.InnerException
                // Nick Chapsas> https://www.youtube.com/watch?v=QKwZlWvfh-o
                // https://github.com/Giorgi/EntityFramework.Exceptions
                foreach (var item in ex.Entries)
                {
                    item.State = EntityState.Detached;
                }
                return false;
            }
        }
    }
}
