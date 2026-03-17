using FluentValidation;

namespace ProductDemo.Features.Products.Queries
{
    public class ProductQueryValidator : AbstractValidator<ProductQuery>
    {
         public ProductQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Product id is required.")
                .MaximumLength(100)
                .WithMessage("Product id must not exceed 100 characters.")
                .Must(id => Guid.TryParse(id, out _))
                .WithMessage("Product id must be a valid GUID.");
        }
    }
}
