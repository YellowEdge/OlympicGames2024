using OgWeb.Models.DTOs;

namespace OgWeb.Repositories;

public interface ICartRepository
{
    Task<int> AddItem(int bookId, int qty);
    Task<int> RemoveItem(int bookId);
    Task<ShoppingCart> GetUserCart();
    Task<int> GetCartItemCount(string userId = "");
    Task<ShoppingCart> GetCart(string userId);
    Task<bool> DoCheckout(CheckoutModel model, bool ispaid, string status);
}