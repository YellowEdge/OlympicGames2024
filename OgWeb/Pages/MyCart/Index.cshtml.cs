using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OgWeb.Data;
using OgWeb.Models;

namespace OgWeb.Pages.MyCart
{
    public class IndexModel : PageModel
    {
        private readonly OgWeb.Data.ApplicationDbContext _context;

        public IndexModel(OgWeb.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ShoppingCart> ShoppingCart { get;set; } = default!;

        public async Task OnGetAsync()
        {
            ShoppingCart = await _context.ShoppingCarts.ToListAsync();
        }
    }
}
