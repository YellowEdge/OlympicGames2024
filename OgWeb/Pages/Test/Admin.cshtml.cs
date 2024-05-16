using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OgWeb.Pages.Test;

[Authorize(Roles = "admin")]
[Authorize(Policy = "TwoFactorEnabled")]
public class AdminModel : PageModel
{
    public void OnGet()
    {
    }
}
