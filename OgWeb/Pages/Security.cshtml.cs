using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OgWeb.Pages;

[Authorize(Roles = "ticketcontrol")]

public class SecurityModel : PageModel
{
    public void OnGet()
    {
    }
}
