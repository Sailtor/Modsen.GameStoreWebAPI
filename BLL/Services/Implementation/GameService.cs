using AutoMapper;
using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using BLL.Infrastructure.Validators;
using BLL.Services.Contracts;
using DAL.Models;
using DAL.Repository.UnitOfWork;
using FluentValidation;

namespace BLL.Services.Implementation
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<GameForCreationDto> _creationValidator;
        private readonly IValidator<GameForUpdateDto> _updateValidator;

        public GameService(IMapper mapper, IUnitOfWork unitOfWork, IValidator<GameForCreationDto> creationValidator, IValidator<GameForUpdateDto> updateValidator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _creationValidator = creationValidator;
            _updateValidator = updateValidator;
        }

        public async Task<IEnumerable<GameForResponceDto>> GetAllGamesAsync()
        {
            return _mapper.Map<IEnumerable<GameForResponceDto>>(await _unitOfWork.Game.GetAllAsync());
        }

        public async Task<GameForResponceDto> GetGameByIdAsync(int gameid)
        {
            return _mapper.Map<GameForResponceDto>(await _unitOfWork.Game.GetByIdIncludeAllAsync(gameid));
        }

        public async Task AddGameAsync(GameForCreationDto gameForCreation)
        {
            _creationValidator.ValidateAndThrowCustom(gameForCreation);
            await _unitOfWork.Game.AddAsync(_mapper.Map<Game>(gameForCreation));
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateGameAsync(GameForUpdateDto gameForUpdate)
        {
            _updateValidator.ValidateAndThrowCustom(gameForUpdate);
            Game game = await _unitOfWork.Game.GetByIdAsync(gameForUpdate.Id);
            _mapper.Map(gameForUpdate, game);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteGameAsync(int gameid)
        {
            _ = await _unitOfWork.Game.GetByIdAsync(gameid);
            await _unitOfWork.Game.Delete(gameid);
            await _unitOfWork.SaveAsync();
        }

        public async Task AddGameGenreAsync(int gameid, int genreid)
        {
            Game gameEntity = await _unitOfWork.Game.GetByIdAsync(gameid);
            Genre genreEntity = await _unitOfWork.Genre.GetByIdAsync(genreid);
            gameEntity.Genres.Add(genreEntity);
            await _unitOfWork.SaveAsync();
        }

        public async Task AddGamePlatformAsync(int gameid, int platformid)
        {
            Game gameEntity = await _unitOfWork.Game.GetByIdAsync(gameid);
            Platform platformEntity = await _unitOfWork.Platform.GetByIdAsync(platformid);
            gameEntity.Platforms.Add(platformEntity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteGamePlatformAsync(int gameid, int platformid)
        {
            Game gameEntity = await _unitOfWork.Game.GetByIdAsync(gameid);
            Platform platformEntity = await _unitOfWork.Platform.GetByIdAsync(platformid);
            gameEntity.Platforms.Remove(platformEntity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteGameGenreAsync(int gameid, int genreid)
        {
            Game gameEntity = await _unitOfWork.Game.GetByIdAsync(gameid);
            Genre genreEntity = await _unitOfWork.Genre.GetByIdAsync(genreid);
            gameEntity.Genres.Remove(genreEntity);
            await _unitOfWork.SaveAsync();
        }
    }
}
