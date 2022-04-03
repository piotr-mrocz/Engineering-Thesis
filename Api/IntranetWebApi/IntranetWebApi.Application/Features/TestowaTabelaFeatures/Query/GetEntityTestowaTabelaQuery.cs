using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Entities;
using IntranetWebApi.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Application.Features.TestowaTabelaFeatures.Query
{
    public class GetEntityTestowaTabelaQuery : IRequest<Response<Test>>
    {
        public int Id { get; set; }
    }

    public class GetAntityTestowaTabelaQueryHandler : IRequestHandler<GetEntityTestowaTabelaQuery, Response<Test>>
    {
        private readonly IGenericRepository<Test> _repo;
        public GetAntityTestowaTabelaQueryHandler(IGenericRepository<Test> repo)
        {
            _repo = repo;
        }

        public async Task<Response<Test>> Handle(GetEntityTestowaTabelaQuery request, CancellationToken cancellationToken)
        {
            var test = await _repo.GetEntityByExpression(x => x.Id == request.Id, cancellationToken);

            return test;
        }
    }
}
