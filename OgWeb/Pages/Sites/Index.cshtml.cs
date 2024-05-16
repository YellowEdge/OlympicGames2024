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
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public IEnumerable<Site> Sites { get; set; }

    public IndexModel(ApplicationDbContext db)
    {
        _db = db;
    }
    public void OnGet()
    {
        Sites = _db.Sites;
    }
}
