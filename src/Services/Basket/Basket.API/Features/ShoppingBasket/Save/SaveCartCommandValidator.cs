namespace Basket.API.Features.ShoppingBasket.Save;

public class SaveCartCommandValidator : AbstractValidator<SaveCartCommand>
{
    public SaveCartCommandValidator()
    {
        RuleFor(x => x.Card)
            .NotNull()
            .WithMessage("Card не должен быть null.");

        RuleFor(x => x.Card.AccountName)
            .NotEmpty()
            .WithMessage("AccountName не должен быть пустым.")
            .MaximumLength(100)
            .WithMessage("AccountName не должен превышать 100 символов.");

        RuleFor(x => x.Card.Items)
            .NotNull()
            .WithMessage("Items не должен быть null.")
            .Must(items => items.Count > 0)
            .WithMessage("Items должен содержать хотя бы один элемент.");

        RuleForEach(x => x.Card.Items).ChildRules(items =>
        {
            items.RuleFor(item => item.ItemId)
                .NotEqual(Guid.Empty)
                .WithMessage("ItemId не должен быть пустым GUID.");
            
            items.RuleFor(item => item.ItemTitle)
                .NotEmpty()
                .WithMessage("ItemTitle не должен быть пустым.")
                .MaximumLength(200)
                .WithMessage("ItemTitle не должен превышать 200 символов.");

            items.RuleFor(item => item.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity должен быть больше 0.");
            
            items.RuleFor(item => item.UnitPrice)
                .GreaterThan(0)
                .WithMessage("UnitPrice должен быть больше 0.");
        });
    }
}