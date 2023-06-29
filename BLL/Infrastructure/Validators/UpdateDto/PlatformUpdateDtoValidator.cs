﻿using BLL.Dtos.InDto;
using FluentValidation;

namespace BLL.Infrastructure.Validators.UpdateDto
{
    public class PlatformUpdateDtoValidator : AbstractValidator<PlatformForUpdateDto>
    {
        public PlatformUpdateDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(m => m.Id).NotNull().GreaterThan(0);
            RuleFor(m => m.Name).NotNull().Length(2, 100);
        }
    }
}