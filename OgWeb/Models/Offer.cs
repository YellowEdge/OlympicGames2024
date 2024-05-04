using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OgWeb.Models;

[Table("Offer")]
public class Offer
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }
    [Display(Name = "Description")]
    [Required]
    [MaxLength(1000)]
    public string Desc { get; set; }
    [Required]
    public int Quantity { get; set; }
}
