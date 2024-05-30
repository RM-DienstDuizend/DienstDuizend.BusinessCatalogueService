using DienstDuizend.BusinessCatalogueService.Infrastructure.Exceptions;

namespace DienstDuizend.BusinessCatalogueService.Infrastructure.Persistence.Repository;

public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> 
    where TEntity : BaseEntity
{
    private readonly ApplicationDbContext _dbContext;

    protected GenericRepository(ApplicationDbContext dbContext)
    {
        this._dbContext = dbContext;
    }
    
    public virtual IQueryable<TEntity> Query()
    {
        return _dbContext.Set<TEntity>();
    }

    public virtual async Task<TEntity> GetByIdOrDefaultAsync(Guid id, bool withTracking = true, CancellationToken cancellationToken = default)
    {
        return withTracking 
            ? await _dbContext.Set<TEntity>()
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken) 
            : await _dbContext.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public virtual async Task<TEntity> GetByIdAsync(Guid id, bool withTracking = true, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdOrDefaultAsync(id, withTracking, cancellationToken);

        if (entity == null)
            throw Error.NotFound<TEntity>();

        return entity;
    }

    public virtual async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<TEntity>()
            .AnyAsync(e => e.Id == id, cancellationToken);
    }

    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
    }

    public virtual void Update(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
    }

    public virtual void Remove(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<TEntity> entities)
    {
        _dbContext.Set<TEntity>().RemoveRange(entities);
    }
}