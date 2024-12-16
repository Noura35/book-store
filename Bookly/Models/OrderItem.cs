namespace Bookly.Models;

public class OrderItem
{
    public int Id { get; set; }
    public int quantity { get; set; }
    public int orderId { get; set; }
    public int price { get; set; }
    public int bookId { get; set; }
    public Order order { get; set; }
    public Book book { get; set; }
    
}