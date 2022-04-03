using AutoMapper;
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
        public string Name { get; set; } = null!;
        public int Number { get; set; }
    }

    public class UpdateTesowaTabelaCommandHandler : IRequestHandler<UpdateTesowaTabelaCommand, ResponseStruct<int>>
    {
        private readonly IGenericRepository<Test> _repo;
        private readonly IMapper _mapper;

        public UpdateTesowaTabelaCommandHandler(IGenericRepository<Test> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
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

            var testToUpdate = _mapper.Map(request, test.Data);

            var response = await _repo.UpdateEntity(testToUpdate, cancellationToken);
            return new ResponseStruct<int>()
            {
                Succeeded = response.Succeeded,
                Message = response.Message
            };
        }
    }
}
