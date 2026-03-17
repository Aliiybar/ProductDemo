using FluentValidation;

namespace ProductDemo.Features.Products.Commands
{
    public class ProductQueryValidator : AbstractValidator<CreateProductCommand>
    {
         public ProductQueryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Product name is required.")
                .MaximumLength(100)
                .WithMessage("Product name must not exceed 100 characters.");
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Product description is required.")
                .MaximumLength(500)
                .WithMessage("Product description must not exceed 500 characters.");
        }
    }
}
