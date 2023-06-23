using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;

namespace BLL.Services.Contracts
{
    public interface IDeveloperService
    {
        Task<IEnumerable<DeveloperForResponceDto>> GetAllDevelopersAsync();
        Task<DeveloperForResponceDto> GetDeveloperByIdAsync(int developerid);
        Task AddDeveloperAsync(DeveloperForCreationDto developer);
        Task UpdateDeveloperAsync(DeveloperForUpdateDto developerForUpdate);
        Task DeleteDeveloperAsync(int developerid);
    }
}