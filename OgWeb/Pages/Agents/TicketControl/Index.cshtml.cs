using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace OgWeb.Pages.Agents.TicketControl;

[Authorize(Roles = "ticketcontrol")]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _db;

    public IndexModel(ApplicationDbContext db)
    {
        _db = db;
    }

    public TicketControlModel TicketData { get; set; }

    [BindProperty]
    [Required]
    [StringLength(72)]
    public string ScannedKey { get; set; }
    public string UserKey { get; set; }

    public int TotalOrders { get; set; }
    public IActionResult OnGet()
    {
        TicketData = new TicketControlModel();

        return Page();
    }

    public async Task OnPostAsync()
    {
        if (ScannedKey == null)
        {
            throw new ArgumentNullException(nameof(ScannedKey));
        }
        int length = 36;

        UserKey = ScannedKey.Substring(0, length);

        TicketData = new TicketControlModel();
        TicketData.orders = await _db.Orders
            .Where(x => x.TicketKey == ScannedKey)
            .Include(x => x.OrderStatus)
            .Include(x => x.OrderDetails)
            .ThenInclude(x => x.Event)
            .ToListAsync();

        TotalOrders = TicketData.orders.ToList().Count;

        if (UserKey != null)
        {
            try
            {
                TicketData.usersByKey = _db.Users.Where(user => user.UserKey == UserKey).Single();
            }
            catch (Exception)
            {

                TicketData.usersByKey = null;
            }
        }


    }
}
