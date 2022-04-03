using AutoMapper;
using IntranetWebApi.Application.Features.TestowaTabelaFeatures.Command;
using IntranetWebApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetWebApi.Infrastructure.Mapping;
public class GeneralProfile : Profile
{
    public GeneralProfile()
    {
        #region Test
        CreateMap<CreateTestowaTabelaCommand, Test>();
        CreateMap<UpdateTesowaTabelaCommand, Test>();
        #endregion
    }
}
 