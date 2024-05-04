using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OgWeb.Pages.MyCart;

public class CartDetailEventPageModel : PageModel
{    
    public SelectList OfferSL { get; set; }

    public void PopulateOffersDropDownList(ApplicationDbContext _context, object selectedQuantity = null)
    {
        var offersQuery = from c in _context.Offers
                              orderby c.Quantity
                              select c;

        OfferSL = new SelectList(offersQuery.AsNoTracking(),
            nameof(Offer.Quantity),
            nameof(Offer.Name),
            selectedQuantity);
    }
}
