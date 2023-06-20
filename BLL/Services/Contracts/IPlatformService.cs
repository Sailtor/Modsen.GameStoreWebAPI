using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;

namespace BLL.Services.Contracts
{
    public interface IPlatformService
    {
        public Task<IEnumerable<PlatformForResponceDto>> GetAllPlatformsAsync();
        public Task<PlatformForResponceDto> GetPlatformByIdAsync(int platformid);
        public Task AddPlatformAsync(PlatformForCreationDto platform);
        public Task UpdatePlatformAsync(int platformid, PlatformForCreationDto platform);
        public Task DeletePlatformAsync(int platformid);
    }
}