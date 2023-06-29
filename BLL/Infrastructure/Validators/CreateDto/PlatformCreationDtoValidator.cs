using BLL.Dtos.InDto;
using FluentValidation;

namespace BLL.Infrastructure.Validators.CreateDto
{
    public class PlatformCreationDtoValidator : AbstractValidator<PlatformForCreationDto>
    {
        public PlatformCreationDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(m => m.Name).NotNull().Length(2, 2047);
        }
    }
}