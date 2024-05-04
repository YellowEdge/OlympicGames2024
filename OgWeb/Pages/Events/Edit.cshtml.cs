using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OgWeb.Pages.Events;

[Authorize(Roles = "admin")]
public class EditModel : CategorieAndSiteNamePageModel
{
    private readonly ApplicationDbContext _context;

    public EditModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Event Event { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Event = await _context.Events
            .Include(s => s.Site)
            .Include(c => c.Category)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (Event == null)
        {
            return NotFound();
        }

        // Select current site and categorie ID.
        PopulateSitesDropDownList(_context, Event.SiteId);
        PopulateCategoriesDropDownList(_context, Event.CategoryId);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var eventToUpdate = await _context.Events.FindAsync(id);

        if (eventToUpdate == null)
        {
            return NotFound();
        }

        if (await TryUpdateModelAsync<Event>(
             eventToUpdate,
             "event",   // Prefix for form value.
             s => s.EventTitle, s => s.EventDesc, s => s.Price, s => s.StartDate, s => s.SiteId, s => s.CategoryId))
        {
            await _context.SaveChangesAsync();
            TempData["success"] = "Event updated successfully";
            return RedirectToPage("./Index");
        }

        // Select DepartmentID if TryUpdateModelAsync fails.
        PopulateSitesDropDownList(_context, eventToUpdate.SiteId);
        PopulateCategoriesDropDownList(_context, eventToUpdate.CategoryId);
        return Page();
    }

}
