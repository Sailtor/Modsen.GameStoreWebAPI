using BLL.Dtos.InDto;
using FluentValidation;

namespace BLL.Infrastructure.Validators.CreateDto
{
    public class GameCreationDtoValidator : AbstractValidator<GameForCreationDto>
    {
        public GameCreationDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(m => m.DeveloperId).GreaterThan(0);
            RuleFor(m => m.Name).NotNull().Length(2, 100);
            RuleFor(m => m.ReleaseDate).GreaterThan(new DateTime(1945, 1, 1)).LessThan(DateTime.Now);
            RuleFor(m => m.Description).NotNull().Length(1, 2047);
            RuleFor(m => m.Price).InclusiveBetween(0,900000);
        }
    }
}
