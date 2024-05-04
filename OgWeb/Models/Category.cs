using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OgWeb.Models;

[Table("Category")]
public class Category
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

    public List<Event> Events { get; set; }
}
