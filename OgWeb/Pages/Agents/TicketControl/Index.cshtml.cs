using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace OgWeb.Pages.Agents.TicketControl;

[Authorize(Policy = "TwoFactorEnabled")]
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
    [Required(ErrorMessage ="Provide a valid key !")]
    [StringLength(72, ErrorMessage = "The key length is not valid !")]
    [MinLength(72, ErrorMessage = "The key length is not valid !")]
    public string? ScannedKey { get; set; }
    public string UserKey { get; set; }

    public int TotalOrders { get; set; } = 0;
    public IActionResult OnGet()
    {
        TicketData = new TicketControlModel();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }

        int length = 36;
        UserKey = ScannedKey.Substring(0, length);

        TicketData = new TicketControlModel();
        TicketData.usersByKey = null;
        TicketData.orders = null;

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
        return Page();
    }
}
