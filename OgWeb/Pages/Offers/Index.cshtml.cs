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

namespace OgWeb.Pages.Offers;

[Authorize(Policy = "TwoFactorEnabled")]
[Authorize(Roles = "admin")]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public IEnumerable<Offer> Offers { get; set; }

    public IndexModel(ApplicationDbContext db)
    {
        _db = db;
    }
    public void OnGet()
    {
        Offers = _db.Offers;
    }
}

