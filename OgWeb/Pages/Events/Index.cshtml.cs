using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OgWeb.Data;
using OgWeb.Models;

namespace OgWeb.Pages.Events;

[Authorize(Policy = "TwoFactorEnabled")]
[Authorize(Roles = "admin")]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration Configuration;

    public IndexModel(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        Configuration = configuration;
    }

    public string NameSort { get; set; }
    public string DateSort { get; set; }
    public string CurrentFilter { get; set; }
    public string CurrentSort { get; set; }

    public PaginatedList<Event> Events { get; set; }

    public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
    {
        CurrentSort = sortOrder;
        NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        DateSort = sortOrder == "Date" ? "date_desc" : "Date";
        if (searchString != null)
        {
            pageIndex = 1;
        }
        else
        {
            searchString = currentFilter;
        }

        CurrentFilter = searchString;

        IQueryable<Event> eventsIQ = from s in _context.Events select s;

        eventsIQ = eventsIQ.Include(s => s.Site).Include(c => c.Category).AsNoTracking();

        if (!String.IsNullOrEmpty(searchString))
        {
            eventsIQ = eventsIQ.Where<Event>(s => s.EventTitle.Contains(searchString) || s.EventDesc.Contains(searchString));
        }
        switch (sortOrder)
        {
            case "name_desc":
                eventsIQ = eventsIQ.OrderByDescending(s => s.EventTitle);
                break;
            case "Date":
                eventsIQ = eventsIQ.OrderBy(s => s.StartDate);
                break;
            case "date_desc":
                eventsIQ = eventsIQ.OrderByDescending(s => s.StartDate);
                break;
            default:
                eventsIQ = eventsIQ.OrderBy(s => s.EventTitle);
                break;
        }

        var pageSize = Configuration.GetValue("PageSize", 4);
        Events = await PaginatedList<Event>.CreateAsync(eventsIQ, pageIndex ?? 1, pageSize);
    }
}