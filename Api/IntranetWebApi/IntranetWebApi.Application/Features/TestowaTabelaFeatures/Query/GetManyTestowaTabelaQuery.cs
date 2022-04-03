using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Entities;
using MediatR;

namespace IntranetWebApi.Application.Features.TestowaTabelaFeatures.Query
{
    public class GetManyTestowaTabelaQuery : IRequest<IEnumerable<Test>>
    {
    }

    public class GetManyTestowaTabelaQueryHandler : IRequestHandler<GetManyTestowaTabelaQuery, IEnumerable<Test>>
    {
        private readonly IGenericRepository<Test> _repo;

        public GetManyTestowaTabelaQueryHandler(IGenericRepository<Test> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Test>> Handle(GetManyTestowaTabelaQuery request, CancellationToken cancellationToken)
        {
            var tests = await _repo.GetManyEntitiesByExpression(x => true, cancellationToken);

            return tests.Data;
        }
    }
}
