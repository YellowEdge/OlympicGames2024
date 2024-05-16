using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OgWeb.Data;
using OgWeb.Models;

namespace OgWeb.Pages.Offers;

[Authorize(Policy = "TwoFactorEnabled")]
[Authorize(Roles = "admin")]
[BindProperties]
public class EditModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public Offer Offer { get; set; }

    public EditModel(ApplicationDbContext db)
    {
        _db = db;
    }
    public void OnGet(int id)
    {
        Offer = _db.Offers.Find(id);
    }

    public async Task<IActionResult> OnPost()
    {
        if (Offer.Name == Offer.Desc)
        {
            ModelState.AddModelError("Offer.Name", "The Description cannot exactly match the Name.");
        }
        if (ModelState.IsValid)
        {
            _db.Offers.Update(Offer);
            await _db.SaveChangesAsync();
            TempData["success"] = "Offer updated successfully";
            return RedirectToPage("Index");
        }
        return Page();

    }
}
