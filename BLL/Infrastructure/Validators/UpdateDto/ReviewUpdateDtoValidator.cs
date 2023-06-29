using BLL.Dtos.InDto;
using FluentValidation;

namespace BLL.Infrastructure.Validators.UpdateDto
{
    public class ReviewUpdateDtoValidator : AbstractValidator<ReviewForUpdateDto>
    {
        public ReviewUpdateDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(m => m.UserId).NotNull().GreaterThan(0);
            RuleFor(m => m.GameId).NotNull().GreaterThan(0);
            RuleFor(m => m.Score).NotNull().InclusiveBetween(1, 5);
            RuleFor(m => m.ReviewText).Length(2, 2047);
        }
    }
}