using BLL.Dtos.InDto;
using FluentValidation;

namespace BLL.Infrastructure.Validators.CreateDto
{
    public class RoleUpdateDtoValidator : AbstractValidator<RoleForUpdateDto>
    {
        public RoleUpdateDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(m => m.Id).GreaterThan(0);
            RuleFor(m => m.Name).NotNull().Length(1, 2047);
        }
    }
}
