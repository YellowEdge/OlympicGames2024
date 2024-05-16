using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OgWeb.Data;
using OgWeb.Models;

namespace OgWeb.Pages.Categories;

[Authorize(Policy = "TwoFactorEnabled")]
[Authorize(Roles = "admin")]
[BindProperties]
public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public Category Category { get; set; }

    public CreateModel(ApplicationDbContext db)
    {
        _db = db;
    }
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if(Category.Name == Category.Desc)
        {
            ModelState.AddModelError("Category.Name", "The Description cannot exactly match the Name.");
        }
        if (ModelState.IsValid)
        {
            await _db.Categories.AddAsync(Category);
            await _db.SaveChangesAsync();
            TempData["success"] = "Category created successfully";
            return RedirectToPage("Index");
        }
        return Page();
        
    }
}
