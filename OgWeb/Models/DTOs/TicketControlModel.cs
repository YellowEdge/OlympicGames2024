namespace OgWeb.Models.DTOs;

public class TicketControlModel
{
    public IEnumerable<Order> orders { get; set; }
    public ApplicationUser usersByKey { get; set; }
}
