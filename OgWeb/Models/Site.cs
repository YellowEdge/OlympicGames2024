using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OgWeb.Models;

[Table("Site")]
public class Site
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(200)]
    public string Addresse { get; set; }
    [Required]
    [MaxLength(50)]
    public string City { get; set; }

    public List<Event> Events { get; set; }
}
