using AutoMapper;
using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using DAL.Models;

namespace BLL.Infrastructure.Automapper.Automapper_Profiles
{
    public class PurchaseProfile : Profile
    {
        public PurchaseProfile()
        {
            CreateMap<PurchaseForCreationDto, Purchase>()
                .ForMember(dest => dest.PurchaseDate, opt => opt.MapFrom(scr => DateTime.Now));
            CreateMap<Purchase, PurchaseForResponceDto>();
        }
    }
}
