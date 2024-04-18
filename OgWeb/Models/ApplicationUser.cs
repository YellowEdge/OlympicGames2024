using Microsoft.AspNetCore.Identity;

namespace OgWeb.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public String Address { get; set; } = String.Empty;
    public DateTime CreatedAt { get; set; }

}
