using System;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation;

public class CarValidations : AbstractValidator<Car>
{
    public CarValidations()
    {
        RuleFor(c => c.Description).NotNull();
        RuleFor(c => c.Description).MinimumLength(2);

        RuleFor(c => c.BrandId).NotNull();
        RuleFor(c => c.ColorId).NotNull();

        RuleFor(c => c.DailyPrice).NotNull();
        RuleFor(c => c.DailyPrice).GreaterThan(0);

        RuleFor(c => c.ModelYear).NotNull();
        RuleFor(c => c.ModelYear).InclusiveBetween(1970, (DateTime.Now.Year + 2));



    }

}