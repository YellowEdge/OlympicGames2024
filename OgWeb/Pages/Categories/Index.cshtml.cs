using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OgWeb.Data;
using OgWeb.Models;

namespace OgWeb.Pages.Categories;

[Authorize(Roles = "admin")]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public IEnumerable<Category> Categories { get; set; }

    public IndexModel(ApplicationDbContext db)
    {
        _db = db;
    }
    public void OnGet()
    {
        Categories = _db.Categories;
    }
}
