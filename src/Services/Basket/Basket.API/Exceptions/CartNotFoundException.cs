using Common.Kernel.Exceptions;

namespace Basket.API.Exceptions;

public class CartNotFoundException(string accountName) : NotFoundException("ShoppingCart", accountName);