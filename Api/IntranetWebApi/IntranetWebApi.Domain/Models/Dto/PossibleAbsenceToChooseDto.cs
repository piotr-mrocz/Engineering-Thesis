﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Domain.Models.Dto;

public class PossibleAbsenceToChooseDto
{
    public int Id { get; set; }
    public string AbsenceName { get; set; } = null!;
}
