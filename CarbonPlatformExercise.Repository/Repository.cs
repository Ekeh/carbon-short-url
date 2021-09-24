using CarbonPlatformExercise.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarbonPlatformExercise.Repository
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> Context { get; }

        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Get(int id);
        Task<TEntity> Insert(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
        Task Remove(TEntity entity);
        Task SaveChanges();
    }

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext context;
        private DbSet<TEntity> entities;
        string errorMessage = string.Empty;
        public IQueryable<TEntity> Context { get { return context.Set<TEntity>(); } }
        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<TEntity>();
        }
        public Task<IEnumerable<TEntity>> GetAll()
        {
            return Task.FromResult(entities.AsEnumerable());
        }

        public async Task<TEntity> Get(int id)
        {
            return await entities.SingleOrDefaultAsync(s => s.Id == id);
        }
        public async Task<TEntity> Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            await entities.AddAsync(entity);
            context.SaveChanges();
            return entity;
        }

        public async Task Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
           await context.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
           await context.SaveChangesAsync();
        }
        public async Task Remove(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
        }

        public async Task SaveChanges()
        {
          await context.SaveChangesAsync();
        }

    }
}
