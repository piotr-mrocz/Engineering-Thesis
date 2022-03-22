using IntranetWebApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IntranetWebApi.Repository;
public class GenericRepository<T> where T : class
{
    private readonly IntranetDbContext _dbContext;

    public GenericRepository(IntranetDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T> GetEntityByExpression(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Set<T>().FirstOrDefaultAsync(expression, cancellationToken);
        return entity;
    }

    public async Task<IEnumerable<T>> GetManyEntitiesByExpression(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
    {
        var entities = await _dbContext.Set<T>().Where(expression).ToListAsync(cancellationToken);
        return entities;
    }

    public async Task<bool> CreateEntity(T createEntity, CancellationToken cancellationToken)
    {
        try
        {

        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public async Task<bool> UpdateEntity(T updateEntity, CancellationToken cancellationToken)
    {

    }

    public async Task<bool> DeleteEntity(T deleteEntity, CancellationToken cancellationToken)
    {

    }
}
