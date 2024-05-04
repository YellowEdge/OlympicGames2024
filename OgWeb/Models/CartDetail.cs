using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OgWeb.Models;

[Table("CartDetail")]
public class CartDetail
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int ShoppingCartId { get; set; }
    [Required]
    public int EventId { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    public double UnitPrice { get; set; }
    public Event Event { get; set; }
    public ShoppingCart ShoppingCart { get; set; }
}
