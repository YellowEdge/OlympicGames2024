using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OgWeb.Models;

[Table("OrderDetail")]
public class OrderDetail
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int OrderId { get; set; }
    [Required]
    public int EventId { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    public double UnitPrice { get; set; }
    public Order Order { get; set; }
    public Event Event { get; set; }
}
