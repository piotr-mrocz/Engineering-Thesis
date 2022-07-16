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
using Microsoft.AspNetCore.SignalR;

namespace IntranetWebApi.Application.Features.TaskFeatures;

public class DeleteTaskCommand : IRequest<BaseResponse>
{
    public int IdTask { get; set; }
}

public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand, BaseResponse>
{
    private readonly IGenericRepository<IntranetWebApi.Domain.Models.Entities.Task> _taskRepo;
    private readonly IGenericRepository<SystemMessage> _systemMessageRepo;
    private readonly IHubContext<SystemMessageHubClient, ISystemMessageHubClient> _systemMessagesHub;

    public DeleteTaskHandler(
        IGenericRepository<IntranetWebApi.Domain.Models.Entities.Task> taskRepo,
        IGenericRepository<SystemMessage> systemMessageRepo,
        IHubContext<SystemMessageHubClient, ISystemMessageHubClient> systemMessagesHub)
    {
        _taskRepo = taskRepo;
        _systemMessageRepo = systemMessageRepo;
        _systemMessagesHub = systemMessagesHub;
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
            await AddSystemMessage(task.Data.IdUser, cancellationToken);

        return new BaseResponse()
        {
            Succeeded = response.Succeeded,
            Message = "Operacja zakończona powodzeniem"
        };
    }

    private async System.Threading.Tasks.Task AddSystemMessage(int idUser, CancellationToken cancellationToken)
    {
        var systemMessage = new SystemMessage()
        {
            IdUser = idUser,
            Info = EnumHelper.GetEnumDescription(SystemMessageTypeEnum.RemoveUserTask),
            AddedDate = DateTime.Now
        };

        var response = await _systemMessageRepo.CreateEntity(systemMessage, cancellationToken);

        if (response.Succeeded)
            await _systemMessagesHub.Clients.All.NewSystemMessage();
    }
}
