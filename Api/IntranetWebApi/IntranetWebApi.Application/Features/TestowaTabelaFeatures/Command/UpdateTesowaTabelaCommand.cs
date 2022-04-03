using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Entities;
using IntranetWebApi.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Application.Features.TestowaTabelaFeatures.Command
{
    public class UpdateTesowaTabelaCommand : IRequest<ResponseStruct<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Number { get; set; }
    }

    public class UpdateTesowaTabelaCommandHandler : IRequestHandler<UpdateTesowaTabelaCommand, ResponseStruct<int>>
    {
        private readonly IGenericRepository<Test> _repo;

        public UpdateTesowaTabelaCommandHandler(IGenericRepository<Test> repo)
        {
            _repo = repo;
        }

        public async Task<ResponseStruct<int>> Handle(UpdateTesowaTabelaCommand request, CancellationToken cancellationToken)
        {
            var test = await _repo.GetEntityByExpression(x => x.Id == request.Id, cancellationToken);

            if (!test.Succeeded)
            {
                return new ResponseStruct<int>()
                {
                    Message = test.Message
                };
            }

            test.Data.Name = string.IsNullOrEmpty(request.Name) ? test.Data.Name : request.Name;
            test.Data.Number = request.Number.HasValue ? request.Number.Value : test.Data.Number;

            var response = await _repo.UpdateEntity(test.Data, cancellationToken);
            return new ResponseStruct<int>()
            {
                Succeeded = response.Succeeded,
                Message = response.Message
            };
        }
    }
}
