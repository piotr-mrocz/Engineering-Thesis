using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    public DeleteTaskHandler(IGenericRepository<IntranetWebApi.Domain.Models.Entities.Task> taskRepo)
    {
        _taskRepo = taskRepo;
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

        return new BaseResponse()
        {
            Succeeded = response.Succeeded,
            Message = response.Succeeded
                    ? "Operacja zakończona powodzeniem"
                    : "Nie udało sięusunąć zadania!"
        };
    }
}
