using Application.Context;
using Domain.Common.Interface;
using Domain.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Common
{
    public class RepositoryAsync<TEntity> : IRepositoryAsync<TEntity> where TEntity : class, IIdentityEntity
    {

        protected readonly IApplicationDbContext applicationDbContext;
        protected readonly DbSet<TEntity> dbSet;

        protected RepositoryAsync(IApplicationDbContext dbContext)
        {
            this.applicationDbContext = dbContext;
            this.dbSet = this.applicationDbContext.Set<TEntity>();
        }

        public void Dispose()
        {
            applicationDbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public virtual async Task<TEntity> AddAsync(TEntity obj)
        {
            var r = await dbSet.AddAsync(obj);
            await CommitAsync();
            return r.Entity;
        }

        public virtual async Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await dbSet.AddRangeAsync(entities);
            return await CommitAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Task.FromResult(dbSet);
        }

        public virtual async Task<TEntity?> GetByIdAsync(Guid id)
        {
            var entity = await dbSet.Where(a => a.Id == id).FirstOrDefaultAsync();

            if (entity == null)
                return null;

            return entity;
        }

        public virtual async Task<bool> RemoveAsync(Guid id)
        {
            TEntity entity = await GetByIdAsync(id);

            if (entity == null) return false;

            return await RemoveAsync(entity) > 0 ? true : false;
        }

        public virtual async Task<int> RemoveAsync(TEntity obj)
        {
            dbSet.Remove(obj);
            return await CommitAsync();
        }

        public virtual async Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
            return await CommitAsync();
        }

        public virtual async Task<int> UpdateAsync(TEntity obj)
        {
            applicationDbContext.Entry(obj).State = EntityState.Modified;
            return await CommitAsync();
        }

        public virtual async Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            dbSet.UpdateRange(entities);
            return await CommitAsync();
        }

        private async Task<int> CommitAsync()
        {
            return await applicationDbContext.SaveChangesAsync();
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await applicationDbContext.SaveChangesAsync();
        }

        public virtual IQueryable<TEntity> AsQueryable() => applicationDbContext.Set<TEntity>().AsQueryable();
    }
}
