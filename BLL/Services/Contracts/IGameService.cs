using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;

namespace BLL.Services.Contracts
{
    public interface IGameService
    {
        public Task<IEnumerable<GameForResponceDto>> GetAllGameAsync();
        public Task<GameForResponceDto> GetGameByIdAsync(int gameid);
        public Task AddGameAsync(GameForCreationDto game);
        public Task UpdateGameAsync(int gameid, GameForCreationDto game);
        public Task UpdateGenreGameformAsync(int gameid, int genreid);
        public Task UpdatePlatformGameAsync(int platformid, int gameid);

        public Task DeletePlatformGameAsync(int gameid, int platformid);
        public Task DeleteGenreGameAsync(int genreid, int gameid);
        public Task DeleteGameAsync(int gameid);

    }
}
