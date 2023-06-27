using BLL.Dtos.InDto;
using BLL.Dtos.OutDto;
using DAL.Models;
using DAL.Models.Query_String_Parameters;

namespace BLL.Services.Contracts
{
    public interface IDeveloperService
    {
        Task<PagedList<DeveloperForResponceDto>> GetAllDevelopersAsync(DeveloperParameters parameters);
        Task<DeveloperForResponceDto> GetDeveloperByIdAsync(int developerid);
        Task AddDeveloperAsync(DeveloperForCreationDto developer);
        Task UpdateDeveloperAsync(DeveloperForUpdateDto developerForUpdate);
        Task DeleteDeveloperAsync(int developerid);
    }
}