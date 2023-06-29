using BLL.Dtos.InDto;
using FluentValidation;

namespace BLL.Infrastructure.Validators.CreateDto
{
    public class ReviewCreationDtoValidator : AbstractValidator<ReviewForCreationDto>
    {
        public ReviewCreationDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(m => m.Score).NotNull().InclusiveBetween(1, 5);
            RuleFor(m => m.ReviewText).Length(2, 2047);
        }
    }
}