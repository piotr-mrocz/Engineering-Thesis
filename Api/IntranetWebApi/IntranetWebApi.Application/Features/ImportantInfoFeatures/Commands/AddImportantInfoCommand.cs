using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Domain.Models.Dto;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Response;
using MediatR;

namespace IntranetWebApi.Application.Features.ImportantInfoFeatures.Commands;

public class AddImportantInfoCommand : IRequest<BaseResponse>
{
    public AddImportantInfoDto ImportantInfoDetails { get; set; } = null!;
}

public class AddImportantInfoHandler : IRequestHandler<AddImportantInfoCommand, BaseResponse>
{
    public IGenericRepository<ImportantInfo> _infoRepo;

    public AddImportantInfoHandler(IGenericRepository<ImportantInfo> infoRepo)
    {
        _infoRepo = infoRepo;
    }

    public async Task<BaseResponse> Handle(AddImportantInfoCommand request, CancellationToken cancellationToken)
    {
        var newInfo = new ImportantInfo()
        {
            Info = request.ImportantInfoDetails.Info,
            StartDate = request.ImportantInfoDetails.StartDate,
            EndDate = request.ImportantInfoDetails.EndDate,
            IdWhoAdded = request.ImportantInfoDetails.IdUser
        };

        var response = await _infoRepo.CreateEntity(newInfo, cancellationToken);

        return new BaseResponse()
        {
            Succeeded = response.Succeeded,
            Message = response.Succeeded ? response.Message : "Nie udało się dodać ważnej informacji"
        };
    }
}
