using Microsoft.EntityFrameworkCore;
using StudentEnrollment.Data.Contracts;
using StudentEnrollment.Data.DbContext;

namespace StudentEnrollment.Data.Repositories;

public class GenericRepository<TEntity>(StudentEnrollmentDbContext context) : IGenericRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly StudentEnrollmentDbContext Context = context;

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await Context.Set<TEntity>().AddAsync(entity);
        await Context.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await GetAsync(id);

        if (entity is not null) Context.Set<TEntity>().Remove(entity);

        return await Context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Exists(int id)
    {
        return await Context.Set<TEntity>().AnyAsync(q => q.Id == id);
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await Context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity?> GetAsync(int? id)
    {
        return await Context.Set<TEntity>().FindAsync(id);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        Context.Set<TEntity>().Update(entity);
        await Context.SaveChangesAsync();
    }
}