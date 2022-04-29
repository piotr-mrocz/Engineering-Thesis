using AutoMapper;
using IntranetWebApi.Domain.Models.Dto;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Application.Features.PresenceFeatures.Commands
{
    public class CreateRangePresencesCommand : IRequest<ResponseStruct<bool>>
    {
        public List<PresenceDto> ListOfPresences { get; set; } = null!;
    }

    public class CreateRangePresencesHandler : IRequestHandler<CreateRangePresencesCommand, ResponseStruct<bool>>
    {
        private readonly IGenericRepository<Presence> _repo;
        private readonly IMapper _mapper;

        public CreateRangePresencesHandler(IGenericRepository<Presence> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ResponseStruct<bool>> Handle(CreateRangePresencesCommand request, CancellationToken cancellationToken)
        {
            var listPresenceToAdd = _mapper.Map<List<PresenceDto>, List<Presence>>(request.ListOfPresences);

            var response = await _repo.CreateRangeEntities(listPresenceToAdd, cancellationToken);

            return response;
        }
    }
}
