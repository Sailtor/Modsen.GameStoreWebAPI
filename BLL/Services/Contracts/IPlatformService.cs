using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;

namespace BLL.Services.Contracts
{
    public interface IPlatformService
    {
        Task<IEnumerable<PlatformForResponceDto>> GetAllPlatformsAsync();
        Task<PlatformForResponceDto> GetPlatformByIdAsync(int platformid);
        Task AddPlatformAsync(PlatformForCreationDto platform);
        Task UpdatePlatformAsync(PlatformForUpdateDto platformForUpdate);
        Task DeletePlatformAsync(int platformid);
    }
}