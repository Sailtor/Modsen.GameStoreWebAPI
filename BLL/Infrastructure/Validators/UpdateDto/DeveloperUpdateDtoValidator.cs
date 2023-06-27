using BLL.Dtos.InDto;
using FluentValidation;

namespace BLL.Infrastructure.Validators.UpdateDto
{
    public class DeveloperUpdateDtoValidator : AbstractValidator<DeveloperForUpdateDto>
    {
        public DeveloperUpdateDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(m => m.Id).GreaterThan(0);
            RuleFor(m => m.Name).NotNull().Length(2, 100);
        }
    }
}
