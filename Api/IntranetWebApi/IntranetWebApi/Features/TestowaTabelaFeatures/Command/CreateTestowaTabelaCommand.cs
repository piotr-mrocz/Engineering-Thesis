using IntranetWebApi.Models.Entities;
using IntranetWebApi.Models.Response;
using IntranetWebApi.Repository;
using MediatR;

namespace IntranetWebApi.Features.TestowaTabelaFeatures.Command;
public class CreateTestowaTabelaCommand : IRequest<ResponseStruct<int>>
{
    public string Name { get; set; } = null!;
    public int Number { get; set; }
}

public class CreateTesowaTabelaCommandHandler : IRequestHandler<CreateTestowaTabelaCommand, ResponseStruct<int>>
{
    private readonly IGenericRepository<Test> _repo;

    public CreateTesowaTabelaCommandHandler(IGenericRepository<Test> repo)
    {
        _repo = repo;
    }

    public async Task<ResponseStruct<int>> Handle(CreateTestowaTabelaCommand request, CancellationToken cancellationToken)
    {
        // zrobić mappowianie - zainstalować automappera
        var test = new Test()
        {
            Name = "test",
            Number = 1
        };
        var response = await _repo.CreateEntity(test, cancellationToken);
        return new ResponseStruct<int>();
    }
}