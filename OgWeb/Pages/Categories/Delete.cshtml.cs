using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OgWeb.Data;
using OgWeb.Models;

namespace OgWeb.Pages.Categories;

[Authorize(Policy = "TwoFactorEnabled")]
[Authorize(Roles = "admin")]
[BindProperties]
public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public Category Category { get; set; }

    public DeleteModel(ApplicationDbContext db)
    {
        _db = db;
    }
    public void OnGet(int id)
    {
        Category = _db.Categories.Find(id);
    }

    public async Task<IActionResult> OnPost()
    {

        var categoryFromDb = _db.Categories.Find(Category.Id);
        if (categoryFromDb != null)
        {
            _db.Categories.Remove(categoryFromDb);
            await _db.SaveChangesAsync();
            TempData["success"] = "Category deleted successfully";
            return RedirectToPage("Index");
        }

        return Page();       
    }
}
