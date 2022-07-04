using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Response;
using MediatR;

namespace IntranetWebApi.Application.Features.TaskFeatures;

public class UpdateTaskCommand : IRequest<BaseResponse>
{
    public int IdTask { get; set; }
    public string NewTitle { get; set; } = null!;
    public string NewDescription { get; set; } = null!;
    public DateTime? Deadline { get; set; }
    public int Status { get; set; }
    public int Priority { get; set; }
}

public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, BaseResponse>
{
    private readonly IGenericRepository<IntranetWebApi.Domain.Models.Entities.Task> _taskRepo;

    public UpdateTaskHandler(IGenericRepository<IntranetWebApi.Domain.Models.Entities.Task> taskRepo)
    {
        _taskRepo = taskRepo;
    }

    public async Task<BaseResponse> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var taskToUpdate = await _taskRepo.GetEntityByExpression(x => x.Id == request.IdTask, cancellationToken);

        if (!taskToUpdate.Succeeded || taskToUpdate.Data is null)
        {
            return new BaseResponse()
            {
                Message = "Nie odnaleziono zadania! Proces wstrzymany!"
            };
        }

        taskToUpdate.Data.Title = request.NewTitle;
        taskToUpdate.Data.Description = request.NewDescription;
        taskToUpdate.Data.Deadline = request.Deadline;
        taskToUpdate.Data.Status = request.Status;
        taskToUpdate.Data.Priority = request.Priority;

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
