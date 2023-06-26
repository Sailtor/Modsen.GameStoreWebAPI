using BLL.Dtos.InDto;
using FluentValidation;

namespace BLL.Infrastructure.Validators.CreateDto
{
    public class DeveloperCreationDtoValidator : AbstractValidator<DeveloperForCreationDto>
    {
        public DeveloperCreationDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(m => m.Name).NotNull().Length(2, 100);
        }
    }
}
