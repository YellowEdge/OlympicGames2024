using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OgWeb.Models;

[Table("Event")]
public class Event
{
    [Key]
    public int Id { get; set; }
    [Required]
    [Display(Name = "Event")]
    [MaxLength(100)]
    public string EventTitle { get; set; }

    [Required]
    [Display(Name = "Description")]
    [MaxLength(1000)]
    public string EventDesc { get; set; }
    [Required]
    public double Price { get; set; }

    [Display(Name = "Start date")]

    public DateTime StartDate { get; set; }
    public int SiteId { get; set; }
    public Site Site { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public List<OrderDetail> OrderDetails { get; set; }
    public List<CartDetail> CartDetails { get; set; }

}
