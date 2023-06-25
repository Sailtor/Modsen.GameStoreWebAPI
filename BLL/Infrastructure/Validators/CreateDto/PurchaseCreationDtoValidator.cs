using BLL.Dtos.InDto;
using FluentValidation;
using FluentValidation.Results;

namespace BLL.Infrastructure.Validators.CreateDto
{
    public class PurchaseCreationDtoValidator : AbstractValidator<PurchaseForCreationDto>
    {
        protected override bool PreValidate(ValidationContext<PurchaseForCreationDto> context, ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                return true;
            }
            result.Errors.Add(new ValidationFailure("", "Model should be null (yes it is, ima developer i decide lol))"));
            return false;
        }
    }
}
