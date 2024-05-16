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

namespace OgWeb.Pages.Sites;

[Authorize(Policy = "TwoFactorEnabled")]
[Authorize(Roles = "admin")]
[BindProperties]
public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public Site Site { get; set; }

    public CreateModel(ApplicationDbContext db)
    {
        _db = db;
    }
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (Site.Addresse == Site.City)
        {
            ModelState.AddModelError("Site.Addresse", "The City cannot exactly match the Addresse.");
        }
        if (ModelState.IsValid)
        {
            await _db.Sites.AddAsync(Site);
            await _db.SaveChangesAsync();
            TempData["success"] = "Site created successfully";
            return RedirectToPage("Index");
        }
        return Page();
    }
}
