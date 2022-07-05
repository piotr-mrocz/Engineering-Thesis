using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntranetWebApi.Application.Helpers;
using IntranetWebApi.Domain.Enums;
using IntranetWebApi.Domain.Models.Dto;
using IntranetWebApi.Domain.Models.Entities;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Enums;
using IntranetWebApi.Models.Response;
using MediatR;
using Task = IntranetWebApi.Domain.Models.Entities.Task;

namespace IntranetWebApi.Application.Features.TaskFeatures.Queries;

public class GetUsersTasksForSupervisorQuery : IRequest<Response<List<TaskUserDto>>>
{
    public int IdSupervisor { get; set; }
    public int Status { get; set; }
}

public class GetUsersTasksForSupervisorHandler : IRequestHandler<GetUsersTasksForSupervisorQuery, Response<List<TaskUserDto>>>
{
    private readonly IGenericRepository<Task> _taskRepo;
    private readonly IGenericRepository<User> _userRepo;
    private readonly IGenericRepository<Photo> _photoRepo;

    public GetUsersTasksForSupervisorHandler(IGenericRepository<Task> taskRepo, 
        IGenericRepository<User> userRepo,
        IGenericRepository<Photo> photoRepo)
    {
        _taskRepo = taskRepo;
        _userRepo = userRepo;
        _photoRepo = photoRepo;
    }

    public async Task<Response<List<TaskUserDto>>> Handle(GetUsersTasksForSupervisorQuery request, CancellationToken cancellationToken)
    {
        var idDepartmentResponse = await GetIdDepartment(request.IdSupervisor, cancellationToken);

        if (!idDepartmentResponse.Succeeded)
        {
            return new()
            {
                Message = idDepartmentResponse.Message,
                Data = new()
            };
        }

        var usersResponse = await GetAllUsersInDepartment(idDepartmentResponse.Data, request.IdSupervisor, cancellationToken);

        if (!usersResponse.Succeeded)
        {
            return new()
            {
                Message = usersResponse.Message,
                Data = new()
            };
        }

        var idsUsers = usersResponse.Data.Select(u => u.Id).ToList();

        var tasks = await _taskRepo.GetManyEntitiesByExpression(x =>
            idsUsers.Contains(x.IdUser) &&
            x.Status == request.Status, cancellationToken);

        var succeeded = tasks.Succeeded && tasks.Data != null;

        if (tasks.Data == null || !tasks.Data.Any())
        {
            return new Response<List<TaskUserDto>>()
            {
                Message = "Brak zadań do wyświetlenia",
                Data = new List<TaskUserDto>()
            };
        }

        if (!succeeded)
        {
            return new Response<List<TaskUserDto>>()
            {
                Message = "Wystąpił błąd podczas wyszukiwania zadań!",
                Data = new List<TaskUserDto>()
            };
        }

        var tasksDto = await PrepareReturnList(tasks.Data, request.Status, usersResponse.Data, cancellationToken);

        return new Response<List<TaskUserDto>>()
        {
            Succeeded = succeeded,
            Data = tasksDto
        };
    }

    private async Task<ResponseStruct<int>> GetIdDepartment(int idSupervisor, CancellationToken cancellationToken)
    {
        var department = await _userRepo.GetEntityByExpression(x => x.Id == idSupervisor, cancellationToken);

        if (department is null || !department.Succeeded || department.Data is null)
        {
            return new()
            {
                Message = "Nie udało się odnaleźć działu!"
            };
        }

        return new()
        {
            Succeeded = true,
            Data = department.Data.IdDepartment
        };
    }

    private async Task<ResponseStruct<List<User>>> GetAllUsersInDepartment(int idDepartment, int idSupervisor, 
        CancellationToken cancellationToken)
    {
        var allDepartmentUsers = await _userRepo.GetManyEntitiesByExpression(x =>
                x.IdDepartment == idDepartment &&
                x.Id != idSupervisor, cancellationToken);

        if (allDepartmentUsers is null || !allDepartmentUsers.Succeeded || allDepartmentUsers.Data is null || !allDepartmentUsers.Data.Any())
        {
            var departmentName = EnumHelper.GetEnumDescription((DepartmentsEnum)idDepartment);

            return new()
            {
                Message = $"Nie odnaleziono żadnych użytkowników pracujących w dziale: {departmentName}",
                Data = new()
            };
        }

        return new()
        {
            Succeeded = true,
            Data = allDepartmentUsers.Data.ToList()
        };
    }

    private async Task<List<TaskUserDto>> PrepareReturnList(IEnumerable<Task> tasks, int status, List<User> users, CancellationToken cancellationToken)
    {
        if (!tasks.Any())
            return new List<TaskUserDto>();

        var idsUsers = users.Select(x => x.Id).ToList();
        var photos = await GetPhotos(idsUsers, cancellationToken);
        tasks = GetOrderByTasks(tasks, status);

        var returnList = new List<TaskUserDto>();

        foreach (var task in tasks)
        {
            var user = users.FirstOrDefault(x => x.Id == task.IdUser);
            var photo = photos.FirstOrDefault(x => x.IdUser == task.IdUser);

            if (user is null || photo is null)
                continue;

            var taskDetails = new TaskUserDto()
            {
                Id = task.Id,
                IdUser = task.IdUser,
                User = $"{user.FirstName} {user.LastName}",
                PhotoName = photo.Name,
                Title = task.Title,
                Description = task.Description,
                Deadline = task.Deadline.HasValue ? task.Deadline.Value.ToString("dd MMMM yyyy HH:mm") : string.Empty,
                AddedDate = task.AddedDate.ToString("dd MMMM yyyy HH:mm"),
                ProgressDate = task.ProgressDate.HasValue ? task.ProgressDate.Value.ToString("dd MMMM yyyy HH:mm") : string.Empty,
                FinishDate = task.FinishDate.HasValue ? task.FinishDate.Value.ToString("dd MMMM yyyy HH:mm") : string.Empty,
                Status = task.Status,
                Priority = task.Priority,
                PriorityDescription = EnumHelper.GetEnumDescription((PriorityEnum)task.Priority)
            };

            returnList.Add(taskDetails);
        }

        return returnList;
    }

    private async Task<List<Photo>> GetPhotos(List<int> idsUsers, CancellationToken cancellationToken)
    {
        var photos = await _photoRepo.GetManyEntitiesByExpression(x => idsUsers.Contains(x.IdUser), cancellationToken);

        if (!photos.Succeeded || photos.Data is null || !photos.Data.Any())
            return new();

        return photos.Data.ToList();
    }

    private List<Task> GetOrderByTasks(IEnumerable<Task> tasks, int status)
    {
        switch (status)
        {
            case (int)TaskStatusEnum.Done:
                return tasks.OrderBy(x => x.FinishDate).ThenBy(x => x.AddedDate).ToList();

            case (int)TaskStatusEnum.InProgress:
                return tasks.OrderBy(x => x.Priority).ThenBy(x => x.ProgressDate).ToList();

            default:
                return tasks.OrderBy(x => x.Priority).ThenBy(x => x.AddedDate).ToList();
        }
    }
}
