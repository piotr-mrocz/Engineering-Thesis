using IntranetWebApi.Models.Response;
using System.Linq.Expressions;

namespace IntranetWebApi.Infrastructure.Repository
{
    public interface IGenericRepository<T> where T: class
    {
        Task<Response<T>> GetEntityByExpression(Expression<Func<T, bool>> expression, CancellationToken cancellationToken);
        Task<Response<IEnumerable<T>>> GetManyEntitiesByExpression(Expression<Func<T, bool>> expression, CancellationToken cancellationToken);
        Task<ResponseStruct<int>> CreateEntity(T createEntity, CancellationToken cancellationToken);
        Task<ResponseStruct<bool>> CreateRangeEntities(List<T> entitiesList, CancellationToken cancellationToken);
        Task<BaseResponse> UpdateEntity(T updateEntity, CancellationToken cancellationToken);
        Task<BaseResponse> DeleteEntity(T deleteEntity, CancellationToken cancellationToken);
    }
}
