using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace OgWeb.Pages.Sales;

[Authorize(Policy = "TwoFactorEnabled")]
[Authorize(Roles = "admin")]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _db;
    public List<SaleOfferMetricModel> OfferMetrics { get; set; }
    public int TotalCount { get; set; }

    public IndexModel(ApplicationDbContext db)
    {
        _db = db;
    }
    public IActionResult OnGetAsync()
    {
        OfferMetrics = new List<SaleOfferMetricModel>();

        var OrderDetailsList = _db.Orders
            .Where(x => x.IsPaid == true)
            .Include(x => x.OrderDetails);

        var ItemsGrp = OrderDetailsList
            .SelectMany(x => x.OrderDetails)
            .GroupBy(x => x.Quantity)
            .Select(group => new
            {
                OfferMetric = group.Key,
                Count = group.Count()
            })
            .OrderBy(x => x.OfferMetric);

        foreach(var item in ItemsGrp)
        {
            OfferMetrics.Add(new() { OfferMetric = item.OfferMetric, Count = item.Count });
        }

        UpdateOfferMetrics();
        TotalCount = OfferMetrics.ToList().Count;
        return Page();
    }

    private void UpdateOfferMetrics()
    {
        foreach (var item in OfferMetrics)
        {
            var offer = _db.Offers.FirstOrDefault(o => o.Quantity == item.OfferMetric);
            if (offer != null)
            {
                item.MetricName = offer.Name;
            }
        }
    }
}
