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

namespace OgWeb.Pages.Sites;

[Authorize(Policy = "TwoFactorEnabled")]
[Authorize(Roles = "admin")]
[BindProperties]
public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public Site Site { get; set; }

    public DeleteModel(ApplicationDbContext db)
    {
        _db = db;
    }
    public void OnGet(int id)
    {
        Site = _db.Sites.Find(id);
    }

    public async Task<IActionResult> OnPost()
    {

        var SiteFromDb = _db.Sites.Find(Site.Id);
        if (SiteFromDb != null)
        {
            _db.Sites.Remove(SiteFromDb);
            await _db.SaveChangesAsync();
            TempData["success"] = "Site deleted successfully";
            return RedirectToPage("Index");
        }

        return Page();
    }
}
