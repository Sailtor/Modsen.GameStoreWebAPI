using AutoMapper;
using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using DAL.Models;

namespace BLL.Infrastructure.Automapper.Automapper_Profiles
{
    public class DeveloperProfile : Profile
    {
        public DeveloperProfile()
        {
            CreateMap<DeveloperForCreationDto, Developer>();
            CreateMap<DeveloperForUpdateDto, Developer>();
            CreateMap<Developer, DeveloperForResponceDto>();
        }
    }
}