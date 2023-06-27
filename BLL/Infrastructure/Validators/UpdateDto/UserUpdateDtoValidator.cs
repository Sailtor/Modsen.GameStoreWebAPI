using BLL.Dtos.InDto;
using FluentValidation;

namespace BLL.Infrastructure.Validators.UpdateDto
{
    public class UserUpdateDtoValidator : AbstractValidator<UserForUpdateDto>
    {
        public UserUpdateDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(m => m.Id).GreaterThan(0);
            RuleFor(m => m.Login).NotNull().Length(1, 2047);
            RuleFor(m => m.Password).NotNull().Length(1, 2047);
            RuleFor(m => m.Email).NotNull().Length(1, 2047).EmailAddress();
        }
    }
}
