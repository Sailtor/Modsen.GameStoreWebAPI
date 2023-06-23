using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;

namespace BLL.Services.Contracts
{
    public interface IDeveloperService
    {
        Task<IEnumerable<DeveloperForResponceDto>> GetAllDevelopersAsync();
        Task<DeveloperForResponceDto> GetDeveloperByIdAsync(int developerid);
        Task AddDeveloperAsync(DeveloperForCreationDto developer);
        Task UpdateDeveloperAsync(int developerid, DeveloperForCreationDto developer);
        Task DeleteDeveloperAsync(int developerid);
    }
}