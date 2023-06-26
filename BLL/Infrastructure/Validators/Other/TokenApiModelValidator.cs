using DAL.Models;
using FluentValidation;

namespace BLL.Infrastructure.Validators.CreateDto
{
    public class TokenApiModelValidator : AbstractValidator<TokenApiModel>
    {
        public TokenApiModelValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(m => m.AccessToken).NotNull();
            RuleFor(m => m.RefreshToken).NotNull();

            //I don't know how to validate tokens :(
        }
    }
}
