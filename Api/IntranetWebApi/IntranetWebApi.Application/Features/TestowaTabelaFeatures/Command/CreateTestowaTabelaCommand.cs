using AutoMapper;
using IntranetWebApi.Infrastructure.Repository;
using IntranetWebApi.Models.Entities;
using IntranetWebApi.Models.Response;
using MediatR;

namespace IntranetWebApi.Application.Features.TestowaTabelaFeatures.Command;
public class CreateTestowaTabelaCommand : IRequest<ResponseStruct<int>>
{
    public string Name { get; set; } = null!;
    public int Number { get; set; }
}

public class CreateTesowaTabelaCommandHandler : IRequestHandler<CreateTestowaTabelaCommand, ResponseStruct<int>>
{
    private readonly IGenericRepository<Test> _repo;
    private readonly IMapper _mapper;

    public CreateTesowaTabelaCommandHandler(IGenericRepository<Test> repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<ResponseStruct<int>> Handle(CreateTestowaTabelaCommand request, CancellationToken cancellationToken)
    {
        var test = _mapper.Map<Test>(request);
        var response = await _repo.CreateEntity(test, cancellationToken);
        return response;
    }
}