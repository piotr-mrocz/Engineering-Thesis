﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Dto
{
    public class LoginDto
    {
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
