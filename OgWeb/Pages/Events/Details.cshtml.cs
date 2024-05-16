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

namespace OgWeb.Pages.Events;

[Authorize(Policy = "TwoFactorEnabled")]
[Authorize(Roles = "admin")]
public class DetailsModel : PageModel
{
    private readonly OgWeb.Data.ApplicationDbContext _context;

    public DetailsModel(OgWeb.Data.ApplicationDbContext context)
    {
        _context = context;
    }

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
}
