using BLL.Dtos.InDto;
using FluentValidation;

namespace BLL.Infrastructure.Validators.CreateDto
{
    public class RoleCreationDtoValidator : AbstractValidator<RoleForCreationDto>
    {
        public RoleCreationDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(m => m.Name).NotNull().Length(2, 2047);
        }
    }
}