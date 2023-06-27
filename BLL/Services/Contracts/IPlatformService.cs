using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using DAL.Models;
using DAL.Models.Query_String_Parameters;

namespace BLL.Services.Contracts
{
    public interface IPlatformService
    {
        Task<PagedList<PlatformForResponceDto>> GetAllPlatformsAsync(PlatformParameters parameters);
        Task<PlatformForResponceDto> GetPlatformByIdAsync(int platformid);
        Task AddPlatformAsync(PlatformForCreationDto platform);
        Task UpdatePlatformAsync(PlatformForUpdateDto platformForUpdate);
        Task DeletePlatformAsync(int platformid);
    }
}