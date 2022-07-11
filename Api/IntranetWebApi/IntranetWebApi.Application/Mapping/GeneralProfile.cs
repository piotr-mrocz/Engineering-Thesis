using AutoMapper;
using IntranetWebApi.Application.Features.ImportantInfoFeatures.Commands;
using IntranetWebApi.Application.Features.PresenceFeatures.Commands;
using IntranetWebApi.Application.Features.TestowaTabelaFeatures.Command;
using IntranetWebApi.Domain.Models.Dto;
using IntranetWebApi.Domain.Models.Entities;
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
        #region Presence
        CreateMap<PresenceDto, Presence>();
        CreateMap<List<PresenceDto>, List<Presence>>();
        CreateMap<UpdatePresenceCommand, Presence>();
        #endregion Presence

        #region Test
        CreateMap<CreateTestowaTabelaCommand, Test>();
        CreateMap<UpdateTesowaTabelaCommand, Test>();
        #endregion
    }
}
