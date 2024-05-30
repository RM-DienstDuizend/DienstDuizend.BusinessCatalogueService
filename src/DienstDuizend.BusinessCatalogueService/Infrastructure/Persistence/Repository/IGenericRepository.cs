namespace DienstDuizend.BusinessCatalogueService.Infrastructure.Persistence.Repository;

public interface IGenericRepository<TEntity> 
    where TEntity : BaseEntity
{
    IQueryable<TEntity> Query();
    Task<TEntity?> GetByIdOrDefaultAsync(Guid id, bool withTracking = true, CancellationToken cancellationToken = default);
    Task<TEntity> GetByIdAsync(Guid id, bool withTracking = true, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);    
    Task AddRangeAsync(IEnumerable<TEntity> entity, CancellationToken cancellationToken = default);    
    void Update(TEntity entity);
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entity);
}