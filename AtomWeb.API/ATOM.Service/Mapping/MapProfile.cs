using ATOM.Core.DTOs;
using ATOM.Core.DTOs.Request;
using ATOM.Core.DTOs.Response;
using ATOM.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOM.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<HelpDemand, AddHelpDemandDto>().ReverseMap();
            CreateMap<HelpCenter, AddHelpCenterDto>().ReverseMap();
            CreateMap<HelpCenter, UpdateHelpCenterDto>().ReverseMap();
            CreateMap<WreckDemand, PeopleLocationDto>().ReverseMap();
        }
    }
}
