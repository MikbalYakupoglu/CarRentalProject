using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class BrandValidation : AbstractValidator<Brand>
    {
        public BrandValidation()
        {
            RuleFor(b => b.BrandName).NotEmpty();
            RuleFor(b => b.BrandName).MinimumLength(2);

        }
    }
}
