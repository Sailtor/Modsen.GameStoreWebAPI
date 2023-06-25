using BLL.Dtos.InDto;
using FluentValidation;

namespace BLL.Infrastructure.Validators.CreateDto
{
    public class GameUpdateDtoValidator : AbstractValidator<GameForUpdateDto>
    {
        public GameUpdateDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(m => m.Id).GreaterThan(0);
            RuleFor(m => m.DeveloperId).GreaterThan(0);
            RuleFor(m => m.Name).NotNull().Length(2, 100);
            RuleFor(m => m.ReleaseDate).GreaterThan(new DateTime(1945, 1, 1));
            RuleFor(m => m.Description).NotNull().Length(1, 2047);
            RuleFor(m => m.Price).GreaterThanOrEqualTo(0);
        }
    }
}
