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

namespace OgWeb.Pages.Events;

[Authorize(Roles = "admin")]
public class CreateModel : CategorieAndSiteNamePageModel
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<CategorieAndSiteNamePageModel> _logger;

    public CreateModel(ApplicationDbContext context, ILogger<CategorieAndSiteNamePageModel> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult OnGet()
    {
        PopulateCategoriesDropDownList(_context);
        PopulateSitesDropDownList(_context);
        return Page();
    }

    [BindProperty]
    public Event Event { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        var emptyEvent = new Event();

        try
        {
            if (await TryUpdateModelAsync<Event>(
                emptyEvent,
                "event",   // Prefix for form value.
                s => s.EventTitle, s => s.EventDesc, s => s.Price, s => s.StartDate, s => s.SiteId, s => s.CategoryId))
            {
                _context.Events.Add(emptyEvent);
                await _context.SaveChangesAsync();
                TempData["success"] = "Event created successfully";
                return RedirectToPage("./Index");
            }
            else
            {
                var validationErrors = ModelState.Values.Where(E => E.Errors.Count > 0)
                    .SelectMany(E => E.Errors)
                    .Select(E => E.ErrorMessage)
                    .ToList();
                if (validationErrors.Any()) { _logger.LogWarning("--> {validationErrors}", validationErrors); }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }

        // Select CategorieID and SiteId if TryUpdateModelAsync fails.
        PopulateCategoriesDropDownList(_context, emptyEvent.CategoryId);
        PopulateSitesDropDownList(_context, emptyEvent.SiteId);
        return Page();
    }
}
