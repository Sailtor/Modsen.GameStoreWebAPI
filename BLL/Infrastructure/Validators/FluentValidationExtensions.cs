using BLL.Exceptions;
using FluentValidation;

namespace BLL.Infrastructure.Validators
{
    public static class FluentValidationExtensions
    {
        public static void ValidateAndThrowCustom<T>(this IValidator<T> validator, T instance)
        {
            var res = validator.Validate(instance);

            if (!res.IsValid)
            {
                var ex = new ValidationException(res.Errors);
                throw new ModelValidationFailedException(ex.Message);
            }
        }
    }
}