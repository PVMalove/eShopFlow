namespace Basket.API.Models;

public class ShoppingCart
{
    public string AccountName { get; set; } = null!;

    public List<ShoppingCartItem> Items { get; set; } = [];

    public decimal TotalPrice => Items.Sum(item => item.UnitPrice * item.Quantity);

    public ShoppingCart() { }
    
    public ShoppingCart(string accountName)
    {
        AccountName = accountName;
    }
}