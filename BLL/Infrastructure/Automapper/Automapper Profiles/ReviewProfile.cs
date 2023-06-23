using AutoMapper;
using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using DAL.Models;

namespace BLL.Infrastructure.Automapper.Automapper_Profiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<ReviewForCreationDto, Review>()
                .ForMember(dest => dest.ReviewDate, opt => opt.MapFrom(scr => DateTime.Now));
            CreateMap<Review, ReviewForResponceDto>();
        }
    }
}
