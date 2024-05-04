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
    public class DetailsModel : PageModel
    {
        private readonly OgWeb.Data.ApplicationDbContext _context;

        public DetailsModel(OgWeb.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public ShoppingCart ShoppingCart { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingcart = await _context.ShoppingCarts.FirstOrDefaultAsync(m => m.Id == id);
            if (shoppingcart == null)
            {
                return NotFound();
            }
            else
            {
                ShoppingCart = shoppingcart;
            }
            return Page();
        }
    }
}
