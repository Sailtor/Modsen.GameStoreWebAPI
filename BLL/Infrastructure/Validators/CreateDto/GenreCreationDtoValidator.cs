using BLL.Dtos.InDto;
using FluentValidation;

namespace BLL.Infrastructure.Validators.CreateDto
{
    public class GenreCreationDtoValidator : AbstractValidator<GenreForCreationDto>
    {
        public GenreCreationDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(m => m.Name).NotNull().Length(2, 1000);
        }
    }
}
