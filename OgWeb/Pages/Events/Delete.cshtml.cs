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

namespace OgWeb.Pages.Temp;

[Authorize(Policy = "TwoFactorEnabled")]
[Authorize(Roles = "admin")]
public class DeleteModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public DeleteModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Event Event { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var event1 = await _context.Events
            .AsNoTracking()
            .Include(s => s.Site)
            .Include(c => c.Category)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (event1 == null)
        {
            return NotFound();
        }
        else
        {
            Event = event1;
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var event1 = await _context.Events.FindAsync(id);
        if (event1 != null)
        {
            Event = event1;
            _context.Events.Remove(Event);
            await _context.SaveChangesAsync();
            TempData["success"] = "Event deleted successfully";
        }
        return RedirectToPage("./Index");
    }
}
