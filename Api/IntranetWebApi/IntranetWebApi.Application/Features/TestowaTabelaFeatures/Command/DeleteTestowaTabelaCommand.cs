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
    public class DeleteTestowaTabelaCommand : IRequest<ResponseStruct<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteTestowaTabelaCommandHandler : IRequestHandler<DeleteTestowaTabelaCommand, ResponseStruct<int>>
    {
        private readonly IGenericRepository<Test> _repo;
        private readonly IMapper _mapper;

        public DeleteTestowaTabelaCommandHandler(IGenericRepository<Test> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ResponseStruct<int>> Handle(DeleteTestowaTabelaCommand request, CancellationToken cancellationToken)
        {
            var test = await _repo.GetEntityByExpression(x => x.Id == request.Id, cancellationToken);

            if (!test.Succeeded)
            {
                return new ResponseStruct<int>()
                {
                    Succeeded = test.Succeeded,
                    Message = test.Message
                };
            }

            var toDelete = _mapper.Map<Test>(test.Data);

            var deleteReposne = await _repo.DeleteEntity(toDelete, cancellationToken);

            return new ResponseStruct<int>()
            {
                Succeeded = deleteReposne.Succeeded,
                Message = deleteReposne.Message
            };
        }
    }
}
