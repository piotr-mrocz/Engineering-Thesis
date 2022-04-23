﻿using IntranetWebApi.Domain.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Infrastructure.Interfaces;
public interface IAccountService
{
    string GenerateToken(LoginDto dto);
}
