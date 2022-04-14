using IntranetWebApi.Data;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Response;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IntranetWebApi.Application.Repository;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly IntranetDbContext _dbContext;

    public GenericRepository(IntranetDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response<T>> GetEntityByExpression(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Set<T>().FirstOrDefaultAsync(expression, cancellationToken);

        return new Response<T>()
        {
            Succeeded = entity != null,
            Message = entity != null ? "Ok" : "Entity not found",
            Data = entity
        };
    }

    public async Task<Response<IEnumerable<T>>> GetManyEntitiesByExpression(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
    {
        var entities = await _dbContext.Set<T>().Where(expression).ToListAsync(cancellationToken);

        return new Response<IEnumerable<T>>()
        {
            Succeeded = entities != null && entities.Any(),
            Message = entities != null ? "Ok" : "Entity not found",
            Data = entities != null && entities.Any() ? entities : null
        };
    }

    public async Task<ResponseStruct<int>> CreateEntity(T createEntity, CancellationToken cancellationToken)
    {
        await _dbContext.Set<T>().AddAsync(createEntity, cancellationToken);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        int idProperty = (int)createEntity.GetType().GetProperty("Id").GetValue(createEntity, null);

        return new ResponseStruct<int>()
        {
            Succeeded = result > 0,
            Message = result > 0 ? "Ok" : "Can't add entity",
            Data = idProperty
        };
    }

    public async Task<BaseResponse> UpdateEntity(T updateEntity, CancellationToken cancellationToken)
    {
        _dbContext.Set<T>().Update(updateEntity);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        
        return new BaseResponse()
        {
            Succeeded = result > 0,
            Message = result > 0 ? "Ok" : "Can't update entity"
        };
    }

    public async Task<BaseResponse> DeleteEntity(T deleteEntity, CancellationToken cancellationToken)
    {
         _dbContext.Set<T>().Remove(deleteEntity);

        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        return new BaseResponse()
        {
            Succeeded = result > 0,
            Message = result > 0 ? "Ok" : "Can't update entity"
        };
    }
}
