using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OgWeb.Data;
using OgWeb.Models;

namespace OgWeb.Pages.Categories;

[Authorize(Policy = "TwoFactorEnabled")]
[Authorize(Roles = "admin")]
[BindProperties]
public class EditModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public Category Category { get; set; }

    public EditModel(ApplicationDbContext db)
    {
        _db = db;
    }
    public void OnGet(int id)
    {
        Category = _db.Categories.Find(id);
    }

    public async Task<IActionResult> OnPost()
    {
        //if(Category.Name == Category.Desc.ToString())
        if(Category.Name == Category.Desc)
        {
            ModelState.AddModelError("Category.Name", "The Description cannot exactly match the Name.");
        }
        if (ModelState.IsValid)
        {
            _db.Categories.Update(Category);
            await _db.SaveChangesAsync();
            TempData["success"] = "Category updated successfully";
            return RedirectToPage("Index");
        }
        return Page();
        
    }
}
