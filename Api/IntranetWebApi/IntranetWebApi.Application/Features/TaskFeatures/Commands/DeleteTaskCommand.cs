using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Application.Helpers;
using IntranetWebApi.Domain.Enums;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Response;
using MediatR;

namespace IntranetWebApi.Application.Features.TaskFeatures;

public class DeleteTaskCommand : IRequest<BaseResponse>
{
    public int IdTask { get; set; }
}

public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand, BaseResponse>
{
    private readonly IGenericRepository<IntranetWebApi.Domain.Models.Entities.Task> _taskRepo;
    private readonly IGenericRepository<SystemMessage> _systemMessageRepo;

    public DeleteTaskHandler(IGenericRepository<IntranetWebApi.Domain.Models.Entities.Task> taskRepo, IGenericRepository<SystemMessage> systemMessageRepo)
    {
        _taskRepo = taskRepo;
        _systemMessageRepo = systemMessageRepo;
    }

    public async Task<BaseResponse> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepo.GetEntityByExpression(x => x.Id == request.IdTask, cancellationToken);

        if (!task.Succeeded || task.Data is null)
        {
            return new BaseResponse()
            {
                Message = "Nie odnaleziono zadania! Proces wstrzymany!"
            };
        }

        var response = await _taskRepo.DeleteEntity(task.Data, cancellationToken);

        if (!response.Succeeded)
        {
            return new BaseResponse()
            {
                Message = "Nie udało się usunąć zadania!"
            };
        }

        if (task.Data.IdUser != task.Data.WhoAdd)
        {
            var systemMessage = new SystemMessage()
            {
                IdUser = task.Data.IdUser,
                Info = EnumHelper.GetEnumDescription(SystemMessageTypeEnum.RemoveUserTask)
            };

            await _systemMessageRepo.CreateEntity(systemMessage, cancellationToken);
        }

        return new BaseResponse()
        {
            Succeeded = response.Succeeded,
            Message = "Operacja zakończona powodzeniem"
        };
    }
}
