using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OgWeb.Data;
using OgWeb.Models;

namespace OgWeb.Pages.Offers;

[Authorize(Policy = "TwoFactorEnabled")]
[Authorize(Roles = "admin")]
[BindProperties]
public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public Offer Offer { get; set; }

    public DeleteModel(ApplicationDbContext db)
    {
        _db = db;
    }
    public void OnGet(int id)
    {
        Offer = _db.Offers.Find(id);
    }

    public async Task<IActionResult> OnPost()
    {

        var offerFromDb = _db.Offers.Find(Offer.Id);
        if (offerFromDb != null)
        {
            _db.Offers.Remove(offerFromDb);
            await _db.SaveChangesAsync();
            TempData["success"] = "Offer deleted successfully";
            return RedirectToPage("Index");
        }

        return Page();
    }
}
