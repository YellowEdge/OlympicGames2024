using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OgWeb.Pages.Events;

public class CategorieAndSiteNamePageModel : PageModel
{
    public SelectList CategorieNameSL { get; set; }
    public SelectList SiteNameSL { get; set; }

    public void PopulateCategoriesDropDownList(ApplicationDbContext _context, object selectedCategorie = null)
    {
        var categoriesQuery = from c in _context.Categories
                               orderby c.Name // Sort by name.
                               select c;

        CategorieNameSL = new SelectList(categoriesQuery.AsNoTracking(),
            nameof(Category.Id),
            nameof(Category.Name),
            selectedCategorie);
    }

    public void PopulateSitesDropDownList(ApplicationDbContext _context, object selectedSite = null)
    {
        var sitesQuery = from s in _context.Sites
                               orderby s.Addresse
                               select s;

        SiteNameSL = new SelectList(sitesQuery.AsNoTracking(),
            nameof(Site.Id),
            nameof(Site.Addresse),
            selectedSite);
    }
}
