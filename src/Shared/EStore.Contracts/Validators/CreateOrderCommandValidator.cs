using EStore.Contracts.Commands;
using FluentValidation;

namespace EStore.Contracts.Validators;

internal sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty();

        RuleFor(x => x.FirstAddressLine)
            .NotEmpty();

        RuleFor(x => x.SecondAddressLine)
            .NotEmpty();

        RuleFor(x => x.Items)
            .NotEmpty();

        RuleForEach(x => x.Items).ChildRules(item =>
        {
            item.RuleFor(x => x.Quantity).NotEmpty();
            item.RuleFor(x => x.ProductId).NotEmpty();
            item.RuleFor(x => x.UnitPrice).NotEmpty();
        });
    }
}