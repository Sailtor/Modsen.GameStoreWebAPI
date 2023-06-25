using BLL.Dtos.InDto;
using FluentValidation;

namespace BLL.Infrastructure.Validators.CreateDto
{
    public class ReviewUpdateDtoValidator : AbstractValidator<ReviewForUpdateDto>
    {
        public ReviewUpdateDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(m => m.UserId).GreaterThan(0);
            RuleFor(m => m.GameId).GreaterThan(0);
            RuleFor(m => m.Score).NotNull().Must(s => s >= 1 || s <= 5);
            RuleFor(m => m.ReviewText).Length(1, 2047);
        }
    }
}
