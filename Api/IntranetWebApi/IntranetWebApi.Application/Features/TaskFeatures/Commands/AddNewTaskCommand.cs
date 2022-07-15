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

    public AddNewTaskHandler(IGenericRepository<IntranetWebApi.Domain.Models.Entities.Task> taskRepo, IGenericRepository<SystemMessage> systemMessageRepo)
    {
        _taskRepo = taskRepo;
        _systemMessageRepo = systemMessageRepo;
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
            var systemMessage = new SystemMessage()
            {
                IdUser = request.IdUser,
                Info = EnumHelper.GetEnumDescription(SystemMessageTypeEnum.AddNewUserTask)
            };

            await _systemMessageRepo.CreateEntity(systemMessage, cancellationToken);
        }

        return new BaseResponse()
        {
            Succeeded = response.Succeeded,
            Message = "Zadanie zostało dodane"
        };
    }
}
