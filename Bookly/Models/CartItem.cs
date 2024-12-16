namespace Bookly.Models;

public class CartItem
{
    public int Id {get;set;}
    public Book Book {get;set;}
    public int quantity {get;set;}
    public string cardId {get;set;}

}