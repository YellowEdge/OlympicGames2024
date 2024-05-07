using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OgWeb.Models;

public class ApplicationUser : IdentityUser
{
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = "";
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = "";
    public string Address { get; set; } = String.Empty;
    public DateTime CreatedAt { get; set; }
    [Required]
    [MaxLength(36)]
    public string UserKey { get; set; }
}
