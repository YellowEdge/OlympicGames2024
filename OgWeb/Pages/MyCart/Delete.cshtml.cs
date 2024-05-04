﻿using System;
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
    public class DeleteModel : PageModel
    {
        private readonly OgWeb.Data.ApplicationDbContext _context;

        public DeleteModel(OgWeb.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingcart = await _context.ShoppingCarts.FindAsync(id);
            if (shoppingcart != null)
            {
                ShoppingCart = shoppingcart;
                _context.ShoppingCarts.Remove(ShoppingCart);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}