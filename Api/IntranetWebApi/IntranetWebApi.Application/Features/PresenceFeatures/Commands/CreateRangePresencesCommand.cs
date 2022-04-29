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

        public CreateRangePresencesHandler(IGenericRepository<Presence> repo)
        {
            _repo = repo;
        }

        public Task<ResponseStruct<bool>> Handle(CreateRangePresencesCommand request, CancellationToken cancellationToken)
        {



            throw new NotImplementedException();
        }
    }
}
