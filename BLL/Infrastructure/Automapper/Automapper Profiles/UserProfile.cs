using AutoMapper;
using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using DAL.Models;

namespace BLL.Infrastructure.Automapper.Automapper_Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserForCreationDto, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(scr => 0))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(scr => 2));
            CreateMap<User, UserForResponceDto>();
        }
    }
}
