using BLL.Dtos.InDto;
using FluentValidation;

namespace BLL.Infrastructure.Validators.CreateDto
{
    public class GenreUpdateDtoValidator : AbstractValidator<GenreForUpdateDto>
    {
        public GenreUpdateDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(m => m.Id).GreaterThan(0);
            RuleFor(m => m.Name).NotNull().Length(2, 1000);
        }
    }
}
