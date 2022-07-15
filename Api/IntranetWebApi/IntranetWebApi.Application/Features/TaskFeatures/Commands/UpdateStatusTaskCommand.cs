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

public class UpdateStatusTaskCommand : IRequest<BaseResponse>
{
    public int IdTask { get; set; }
    public int Status { get; set; }
}

public class UpdateStatusTaskHandler : IRequestHandler<UpdateStatusTaskCommand, BaseResponse>
{
    private readonly IGenericRepository<IntranetWebApi.Domain.Models.Entities.Task> _taskRepo;
    private readonly IGenericRepository<SystemMessage> _systemMessageRepo;

    public UpdateStatusTaskHandler(IGenericRepository<IntranetWebApi.Domain.Models.Entities.Task> taskRepo, IGenericRepository<SystemMessage> systemMessageRepo)
    {
        _taskRepo = taskRepo;
        _systemMessageRepo = systemMessageRepo;
    }

    public async Task<BaseResponse> Handle(UpdateStatusTaskCommand request, CancellationToken cancellationToken)
    {
        var taskToUpdate = await _taskRepo.GetEntityByExpression(x => x.Id == request.IdTask, cancellationToken);

        if (!taskToUpdate.Succeeded || taskToUpdate.Data is null)
        {
            return new BaseResponse()
            {
                Message = "Nie odnaleziono zadania! Proces wstrzymany!"
            };
        }

        taskToUpdate.Data.Status = request.Status;

        if (request.Status == (int)TaskStatusEnum.InProgress)
            taskToUpdate.Data.ProgressDate = DateTime.Now;

        if (request.Status == (int)TaskStatusEnum.Done)
            taskToUpdate.Data.FinishDate = DateTime.Now;

        var response = await _taskRepo.UpdateEntity(taskToUpdate.Data, cancellationToken);

        if (!response.Succeeded)
        {
            return new BaseResponse()
            {
                Message = "Nie udało się zmienić statusu zadania!"
            };
        }

        if (taskToUpdate.Data.IdUser != taskToUpdate.Data.WhoAdd)
        {
            var systemMessage = new SystemMessage()
            {
                IdUser = taskToUpdate.Data.IdUser,
                Info = request.Status == (int)TaskStatusEnum.InProgress
                     ? EnumHelper.GetEnumDescription(SystemMessageTypeEnum.StartUserTask)
                     : EnumHelper.GetEnumDescription(SystemMessageTypeEnum.EndUserTask),
                AddedDate = DateTime.Now
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
