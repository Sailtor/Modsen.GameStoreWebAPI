using BLL.Dtos.InDto;
using FluentValidation;

namespace BLL.Infrastructure.Validators.CreateDto
{
    public class UserLoginDtoValidator : AbstractValidator<UserForLoginDto>
    {
        public UserLoginDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(m => m.Login).NotNull().Length(1, 2047);
            RuleFor(m => m.Password).NotNull().Length(1, 2047);
        }
    }
}
