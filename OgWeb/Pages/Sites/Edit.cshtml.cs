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

namespace OgWeb.Pages.Sites;

[Authorize(Roles = "admin")]
[BindProperties]
public class EditModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public Site Site { get; set; }

    public EditModel(ApplicationDbContext db)
    {
        _db = db;
    }
    public void OnGet(int id)
    {
        Site = _db.Sites.Find(id);
    }

    public async Task<IActionResult> OnPost()
    {
        if (Site.Addresse == Site.City)
        {
            ModelState.AddModelError("Site.Addresse", "The City cannot exactly match the Addresse.");
        }
        if (ModelState.IsValid)
        {
            _db.Sites.Update(Site);
            await _db.SaveChangesAsync();
            TempData["success"] = "Site updated successfully";
            return RedirectToPage("Index");
        }
        return Page();

    }
}
