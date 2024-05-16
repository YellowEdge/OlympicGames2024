using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OgWeb.Data;
using OgWeb.Models;

namespace OgWeb.Pages.MyCart;

[Authorize(Policy = "TwoFactorEnabled")]
[Authorize(Roles = "admin,client")]
[BindProperties]
public class EditModel : CartDetailEventPageModel
{
    private readonly ApplicationDbContext _db;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<CartDetailEventPageModel> _logger;

    public EditModel(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor,
        UserManager<ApplicationUser> userManager, ILogger<CartDetailEventPageModel> logger)
    {
        _db = context;
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _logger = logger;
    }

    public CartDetail CartDetailEx { get; set; }
    public Event EventEx { get; set; }
    public int ShoppingCartIdEx { get; set; }
    private string GetUserId()
    {
        var principal = _httpContextAccessor.HttpContext.User;
        string userId = _userManager.GetUserId(principal);
        return userId;
    }
    private async Task<ShoppingCart> GetCart(string userId)
    {
        var cart = await _db.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == userId);
        return cart;
    }
    public async Task<IActionResult> OnGetAsync(int eventId)
    {

        string userId = GetUserId();
        PopulateOffersDropDownList(_db);
        try
        {
            using var transaction = _db.Database.BeginTransaction();
            if (userId == null)
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            //throw new Exception("Invalid userid");

            //var shoppingcart = await _db.ShoppingCarts.Where(s => s.UserId == userId).FirstOrDefaultAsync();
            var shoppingcart = await GetCart(userId);
            if (shoppingcart is null)
            {
                shoppingcart = new ShoppingCart
                {
                    UserId = userId
                };
                _db.ShoppingCarts.Add(shoppingcart);
                _db.SaveChanges();
                transaction.Commit();
                shoppingcart = await GetCart(userId);
            }

            ShoppingCartIdEx = shoppingcart.Id;

            var cartdetail = await _db.CartDetails.Where(c => c.ShoppingCartId == shoppingcart.Id).Where(c => c.EventId == eventId)
                                            .Include(c => c.Event)
                                            .ThenInclude(c => c.Site)
                                            .Include(c => c.Event)
                                            .ThenInclude(c => c.Category)
                                            .FirstOrDefaultAsync();
            
            
            if (cartdetail is not null)
            {
                CartDetailEx = cartdetail;
                Console.WriteLine(cartdetail.UnitPrice);
                return Page();
            }
            else
            {
                var event1 = _db.Events.Where(e => e.Id == eventId)
                    .Include(e => e.Site)
                    .Include(e => e.Category)                        
                    .FirstOrDefault();

                EventEx = event1;
                return Page();
            }            
        }
        catch (Exception)
        {
            throw;
        }
}


    public async Task<IActionResult> OnPostUpdateAsync()
    {
        //string userId = GetUserId();
        //var shoppingcart = await GetCart(userId);
        try
        {
            var cartdetail = await _db.CartDetails.Where(c => c.ShoppingCartId == CartDetailEx.ShoppingCartId).Where(c => c.EventId == CartDetailEx.EventId).FirstOrDefaultAsync();
            cartdetail.Quantity = CartDetailEx.Quantity;

            _db.SaveChanges();

            TempData["success"] = "Event added to cart successfully";
            return RedirectToPage("/Ticketing/index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            PopulateOffersDropDownList(_db, CartDetailEx.Quantity);
            return Page();
            throw;
        }        
    }

    public async Task<IActionResult> OnPostAddAsync()
    {
        string userId = GetUserId();
        var shoppingcart = await GetCart(userId);

        var emptyCartItem = new CartDetail()
        {
            EventId = EventEx.Id,
            ShoppingCartId = shoppingcart.Id,
            Quantity = CartDetailEx.Quantity,
            UnitPrice = EventEx.Price
        };

        try
        {
            if (emptyCartItem is not null)
            {
                _db.CartDetails.Add(emptyCartItem);
                await _db.SaveChangesAsync();
                TempData["success"] = "Event added to cart successfully";
                return RedirectToPage("/Ticketing/index");
            }
            else
            {
                var validationErrors = ModelState.Values.Where(E => E.Errors.Count > 0)
                    .SelectMany(E => E.Errors)
                    .Select(E => E.ErrorMessage)
                    .ToList();
                if (validationErrors.Any()) { _logger.LogWarning("--> {validationErrors}", validationErrors); }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }

        PopulateOffersDropDownList(_db, emptyCartItem.Quantity);
        return Page();
    }
}
