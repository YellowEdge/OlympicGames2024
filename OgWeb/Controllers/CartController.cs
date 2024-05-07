using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OgWeb.Repositories;

namespace OgWeb.Controllers;

[Authorize]
public class CartController : Controller
{
    private readonly ICartRepository _cartRepo;

    public CartController(ICartRepository cartRepo)
    {
        _cartRepo = cartRepo;
    }
    public async Task<IActionResult> AddItem(int eventId, int qty = 1, int redirect = 0)
    {
        var cartCount = await _cartRepo.AddItem(eventId, qty);
        if (redirect == 0)
            return Ok(cartCount);
        return RedirectToAction("GetUserCart");
    }

    public async Task<IActionResult> RemoveItem(int eventId)
    {
        var cartCount = await _cartRepo.RemoveItem(eventId);
        return RedirectToAction("GetUserCart");
    }
    public async Task<IActionResult> GetUserCart()
    {
        var cart = await _cartRepo.GetUserCart();
        return View(cart);
    }

    public async Task<IActionResult> GetTotalItemInCart()
    {
        int cartItem = await _cartRepo.GetCartItemCount();
        return Ok(cartItem);
    }

    public IActionResult Checkout()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Checkout(CheckoutModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        bool ispaid = false; string status = "Pending";
        if (model.PaymentMethod == "Paypal")
		{
			var cart = await _cartRepo.GetUserCart();
			var Total = cart.CartDetails.Select(item => item.Event.Price * item.Quantity).Sum();
			TempData["Total"] = Total.ToString();
            TempData["Name"] = model.Name;
            TempData["Email"] = model.Email;
			TempData["MobileNumber"] = model.MobileNumber;
            TempData["Address"] = model.Address;
            TempData["PaymentMethod"] = model.PaymentMethod;

            return RedirectToPage("/Mycart/MyCheckout", new { name = model.Name, total = Total });
        }else if (model.PaymentMethod == "Online")
        {
            ispaid = true;
            status = "Delivered";
        }
		
		var isCheckedOut = await _cartRepo.DoCheckout(model, ispaid, status);
                
        if (!isCheckedOut)
            return RedirectToAction(nameof(OrderFailure));
        return RedirectToAction(nameof(OrderSuccess));
    }

    public IActionResult OrderSuccess()
    {
        return View();
    }

    public IActionResult OrderFailure()
    {
        return View();
    }

}
