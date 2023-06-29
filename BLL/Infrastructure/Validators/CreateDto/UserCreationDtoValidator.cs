using BLL.Dtos.InDto;
using FluentValidation;

namespace BLL.Infrastructure.Validators.CreateDto
{
    public class UserCreationDtoValidator : AbstractValidator<UserForCreationDto>
    {
        public UserCreationDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(m => m.Login).NotNull().Length(2, 2047);
            RuleFor(m => m.Password).NotNull().Length(2, 2047);
            RuleFor(m => m.Email).NotNull().Length(2, 2047).EmailAddress();
        }
    }
}
