using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialHandling.Data
{
    [Validator(typeof(MaterialValidator))]
    public partial class Material
    {
    }

    public class MaterialValidator : AbstractValidator<Material>
    {
        public MaterialValidator()
        {
            /*Part Number must be entered and be between 6 and 16 characters*/
            RuleFor(x => x.PartNum)
                .NotEmpty()
                .Length(6, 16)
                .WithMessage("Must have Valid Part Number");
            /*Aisle must have input that is now equal to the part number and is
             *4 or 5 characters*/
            RuleFor(x => x.Aisle)
                .NotNull()
                .NotEqual(x=> x.PartNum).WithMessage("Must not be equal to PartNum")
                .Length(4,5)
                .WithMessage("Please input valid Aisle Number");
            /*Quantity must be entered and be between 0 and 101*/
            RuleFor(x => x.Qty)
                .NotNull()
                .WithMessage("Must have Numeric Depth")
                .InclusiveBetween(1, 100)
                .WithMessage("Must have Valid Quanity");
        }
    }
}
