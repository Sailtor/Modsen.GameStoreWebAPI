using AutoMapper;
using BLL.Dtos.OutDto;
using DAL.Models;

namespace BLL.Infrastructure.Automapper.Automapper_Profiles
{
    public class GameScoreValueResolver : IValueResolver<Game, GameForResponceDto, double?>
    {
        public double? Resolve(Game source, GameForResponceDto destination, double? destMember, ResolutionContext context)
        {
            {
                if (source.Reviews.Count == 0)
                {
                    return null;
                }
                double intermScore = 0;
                foreach (Review review in source.Reviews)
                {
                    intermScore += review.Score;
                }
                intermScore /= source.Reviews.Count;
                return Math.Round(intermScore, 1);
            }
        }
    }
}