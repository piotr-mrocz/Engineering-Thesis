using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Domain.Enums;
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

    public UpdateStatusTaskHandler(IGenericRepository<IntranetWebApi.Domain.Models.Entities.Task> taskRepo)
    {
        _taskRepo = taskRepo;
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

        return new BaseResponse()
        {
            Succeeded = response.Succeeded,
            Message = response.Succeeded
                    ? "Operacja zakończona powodzeniem"
                    : "Wystąpił błąd! Prosimy o kontakt z działem IT"
        };
    }
}
