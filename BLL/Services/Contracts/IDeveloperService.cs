using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;

namespace BLL.Services.Contracts
{
    public interface IDeveloperService
    {
        public Task<IEnumerable<DeveloperForResponceDto>> GetAllDevelopersAsync();
        public Task<DeveloperForResponceDto> GetDeveloperByIdAsync(int developerid);
        public Task AddDeveloperAsync(DeveloperForCreationDto developer);
        public Task UpdateDeveloperAsync(int developerid, DeveloperForCreationDto developer);
        public Task DeleteDeveloperAsync(int developerid);
    }
}