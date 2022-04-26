using IntranetWebApi.Domain.Models.Dto;
using IntranetWebApi.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Infrastructure.Interfaces;
public interface IAccountService
{
    Task<AuthenticationResponse> GenerateToken(LoginDto dto, CancellationToken cancellationToken);
}
