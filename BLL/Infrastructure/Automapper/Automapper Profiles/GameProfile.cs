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
            CreateMap<GameForUpdateDto, Game>();
            CreateMap<Game, GameForResponceDto>();
                /*.ForMember(dest => dest.Score, opt => opt.MapFrom((src, dest, score) =>
                {
                    byte intermScore = 0;
                    foreach (Review review in src.Reviews)
                    {
                        intermScore += review.Score;
                    }
                    intermScore /= src.Reviews.Count;
                }
                ));*/
        }
    }
}
