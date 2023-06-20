using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;

namespace BLL.Services.Contracts
{
    public interface IGameService
    {
        public Task<IEnumerable<GameForResponceDto>> GetAllGamesAsync();
        public Task<GameForResponceDto> GetGameByIdAsync(int gameid);
        public Task AddGameAsync(GameForCreationDto game);
        public Task UpdateGameAsync(int gameid, GameForCreationDto game);
        public Task AddGameGenreAsync(int gameid, int genreid);
        public Task AddGamePlatformAsync(int gameid, int platformid);
        public Task DeleteGamePlatformAsync(int gameid, int platformid);
        public Task DeleteGameGenreAsync(int gameid, int genreid);
        public Task DeleteGameAsync(int gameid);

    }
}
