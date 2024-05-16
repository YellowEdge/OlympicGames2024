using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OgWeb.Data;
using OgWeb.Models;

namespace OgWeb.Pages.Offers;

[Authorize(Policy = "TwoFactorEnabled")]
[Authorize(Roles = "admin")]
[BindProperties]
public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public Offer Offer { get; set; }

    public CreateModel(ApplicationDbContext db)
    {
        _db = db;
    }
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (Offer.Name == Offer.Desc)
        {
            ModelState.AddModelError("Offer.Name", "The Description cannot exactly match the Name.");
        }
        if (ModelState.IsValid)
        {
            await _db.Offers.AddAsync(Offer);
            await _db.SaveChangesAsync();
            TempData["success"] = "Offer created successfully";
            return RedirectToPage("Index");
        }
        return Page();
    }
}
