namespace Bookly.Models;

public class Order
{
    public int Id { get; set; }
    public List<OrderItem> orderItems { get; set; } = new();
    public int OrderTotal { get; set; } 
    public DateTime OrderPlaced { get; set; }

}