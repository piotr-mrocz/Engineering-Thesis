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
public class AddNewTaskCommand : IRequest<BaseResponse>
{
    public int IdUser { get; set; }
    public int WhoAdd { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime? Deadline { get; set; }
    public int Priority { get; set; }
}

public class AddNewTaskHandler : IRequestHandler<AddNewTaskCommand, BaseResponse>
{
    private readonly IGenericRepository<IntranetWebApi.Domain.Models.Entities.Task> _taskRepo;
    private readonly IGenericRepository<SystemMessage> _systemMessageRepo;
    private readonly IHubContext<SystemMessageHubClient, ISystemMessageHubClient> _systemMessagesHub;
    private readonly IHubContext<TaskHubClient, ITaskHubClient> _taskHub;

    public AddNewTaskHandler(
        IGenericRepository<IntranetWebApi.Domain.Models.Entities.Task> taskRepo, 
        IGenericRepository<SystemMessage> systemMessageRepo,
        IHubContext<SystemMessageHubClient, ISystemMessageHubClient> systemMessagesHub,
        IHubContext<TaskHubClient, ITaskHubClient> taskHub)
    {
        _taskRepo = taskRepo;
        _systemMessageRepo = systemMessageRepo;
        _systemMessagesHub = systemMessagesHub;
        _taskHub = taskHub;
    }

    public async Task<BaseResponse> Handle(AddNewTaskCommand request, CancellationToken cancellationToken)
    {
        var taskToAdd = new IntranetWebApi.Domain.Models.Entities.Task()
        {
            IdUser = request.IdUser,
            Title = request.Title,
            Description = request.Description,
            Deadline = request.Deadline,
            AddedDate = DateTime.Now,
            ProgressDate = null,
            FinishDate = null,
            Status = (int)TaskStatusEnum.ToDo,
            Priority = request.Priority,
            WhoAdd = request.WhoAdd
        };

        var response = await _taskRepo.CreateEntity(taskToAdd, cancellationToken);

        if (!response.Succeeded)
        {
            return new BaseResponse()
            {
                Message = "Nie udało się dodać zadania!"
            };
        }

        if (request.IdUser != request.WhoAdd)
        {
            await AddSystemMessage(request.IdUser, cancellationToken);
        }

        await _taskHub.Clients.All.TaskChanges();

        return new BaseResponse()
        {
            Succeeded = response.Succeeded,
            Message = "Zadanie zostało dodane"
        };
    }

    private async System.Threading.Tasks.Task AddSystemMessage(int idUser, CancellationToken cancellationToken)
    {
        var systemMessage = new SystemMessage()
        {
            IdUser = idUser,
            Info = EnumHelper.GetEnumDescription(SystemMessageTypeEnum.AddNewUserTask),
            AddedDate = DateTime.Now
        };

        var response = await _systemMessageRepo.CreateEntity(systemMessage, cancellationToken);

        if (response.Succeeded)
            await _systemMessagesHub.Clients.All.NewSystemMessage();
    }
}
