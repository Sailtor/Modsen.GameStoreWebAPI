using AutoMapper;
using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using DAL.Models;

namespace BLL.Infrastructure.Automapper.Automapper_Profiles
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<GameForCreationDto, Game>();
            //.ForMember(dest => dest.Id, opt => opt.MapFrom(scr => 0));
            CreateMap<Game, GameForResponceDto>()
                .ForMember(dest => dest.Score, opt => opt.MapFrom((src, dest, score) =>
                {
                    byte intermScore = 0;
                    foreach (Review review in src.Reviews)
                    {
                        intermScore += review.Score;
                    }
                    intermScore /= src.Reviews.Count;
                }
                ));
        }
    }
}
