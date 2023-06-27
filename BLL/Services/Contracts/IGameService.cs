using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using DAL.Models;
using DAL.Models.Query_String_Parameters;

namespace BLL.Services.Contracts
{
    public interface IGameService
    {
        Task<PagedList<GameForResponceDto>> GetAllGamesAsync(GameParameters parameters);
        Task<GameForResponceDto> GetGameByIdAsync(int gameid);
        Task AddGameAsync(GameForCreationDto game);
        Task UpdateGameAsync(GameForUpdateDto gameForUpdate);
        Task AddGameGenreAsync(int gameid, int genreid);
        Task AddGamePlatformAsync(int gameid, int platformid);
        Task DeleteGamePlatformAsync(int gameid, int platformid);
        Task DeleteGameGenreAsync(int gameid, int genreid);
        Task DeleteGameAsync(int gameid);

    }
}
