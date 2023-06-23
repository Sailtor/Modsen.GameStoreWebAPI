using AutoMapper;
using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using DAL.Models;

namespace BLL.Infrastructure.Automapper.Automapper_Profiles
{
    public class PlatformProfile : Profile
    {
        public PlatformProfile()
        {
            CreateMap<PlatformForCreationDto, Platform>();
            CreateMap<Platform, PlatformForResponceDto>();
        }
    }
}
