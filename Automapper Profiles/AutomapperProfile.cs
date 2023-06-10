using AutoMapper;
using GameStoreWebAPI.Models.Dtos.In;

namespace GameStoreWebAPI.Models
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<PlatformForCreationDto, Platform>();
            /*CreateMap<User, UserForCreationDto>();
            CreateMap<UserForCreationDto, User>().ForMember(dest => dest.Id, opt => opt.MapFrom(scr => 0));
            CreateMap<User, UserForResponceDto>();*/

        }
    }
}