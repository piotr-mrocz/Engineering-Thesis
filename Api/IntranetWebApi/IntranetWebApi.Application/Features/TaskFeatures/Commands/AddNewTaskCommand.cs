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

    public AddNewTaskHandler(IGenericRepository<IntranetWebApi.Domain.Models.Entities.Task> taskRepo)
    {
        _taskRepo = taskRepo;
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
            Priority = request.Priority
        };

        var response = await _taskRepo.CreateEntity(taskToAdd, cancellationToken);

        return new BaseResponse()
        {
            Succeeded = response.Succeeded,
            Message = response.Succeeded
                    ? "Zadanie zostało dodane"
                    : "Nie udało się dodać zadania!"
        };
    }
}
