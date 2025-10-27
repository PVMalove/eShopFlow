namespace Basket.API.Features.ShoppingBasket.Remove;

public class RemoveCartCommandValidator : AbstractValidator<RemoveCartCommand>
{
    public RemoveCartCommandValidator()
    {
        RuleFor(x => x.AccountName)
            .NotEmpty()
            .WithMessage("AccountName не должен быть пустым.");
    }
}