using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OgWeb.Pages;

[Authorize(Roles = "client")]
public class ClientModel : PageModel
{
    public void OnGet()
    {
    }
}
