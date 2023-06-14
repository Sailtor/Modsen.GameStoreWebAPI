using AutoMapper;
using GameStoreWebAPI.Models.Dtos.In;
using GameStoreWebAPI.Models.Dtos.Out;

namespace GameStoreWebAPI.Models
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<PlatformForCreationDto, Platform>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(scr => 0));
            CreateMap<Platform, PlatformForResponceDto>();

            CreateMap<GenreForCreationDto, Genre>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(scr => 0));
            CreateMap<Genre, GenreForResponceDto>();

            CreateMap<DeveloperForCreationDto, Developer>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(scr => 0));
            CreateMap<Developer, DeveloperForResponceDto>();

            CreateMap<RoleForCreationDto, Role>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(scr => 0));
            CreateMap<Role, RoleForResponceDto>();

            CreateMap<UserForCreationDto, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(scr => 0))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(scr => 1));
            CreateMap<User, UserForResponceDto>();

            /*CreateMap<UserForLoginDto, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(scr => 0))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(scr => 0))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(scr => ""));
            CreateMap<User, UserForLoginDto>();*/

            CreateMap<PurchaseForCreationDto, Purchase>()
                .ForMember(dest => dest.PurchaseDate, opt => opt.MapFrom(scr => DateTime.Now));
            CreateMap<Purchase, PurchaseForResponceDto>();

            CreateMap<ReviewForCreationDto, Review>()
                .ForMember(dest => dest.ReviewDate, opt => opt.MapFrom(scr => DateTime.Now));
            CreateMap<Review, ReviewForResponceDto>();

            CreateMap<GameForCreationDto, Game>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(scr => 0));
            CreateMap<Game, GameForResponceDto>();
        }
    }
}