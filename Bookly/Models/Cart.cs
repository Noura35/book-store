using Bookly.Data;
using Microsoft.EntityFrameworkCore;

namespace Bookly.Models;

public class Cart
{
    private readonly BooklyContext _context;

    public Cart(BooklyContext context)
    {
        _context = context;
    }

    public string Id {get;set;}
    public List<CartItem> CardItems{get;set;}

    public static Cart getCart(IServiceProvider services)
    {
        ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
        var context = services.GetService<BooklyContext>();
        string cartId = session.GetString("Id") ?? Guid.NewGuid().ToString();
        session.SetString("Id", cartId);
        return new Cart(context){Id = cartId};
    }


    public List<CartItem> getAllCardItems()
    {
        return CardItems ?? (CardItems = _context.CartItems.Where(c1 => c1.cardId == Id)
            .Include(c1=>c1.Book)
            .ToList());
    }



    public CartItem getCardItem(Book book)
    {
        return _context.CartItems.SingleOrDefault(c1 =>
            c1.Book.Id == book.Id && c1.cardId == Id);
    }
    
    

    public void AddToCart(Book book, int qt)
    {
        var cartItem = getCardItem(book);
        if (cartItem == null)
        {
            cartItem = new CartItem
            {
                Book = book,
                quantity = qt,
                cardId = Id
            };
            
            _context.CartItems.Add(cartItem);
        }
        else
        {
            cartItem.quantity += qt;
        }
        
        _context.SaveChanges();
    }
    
    
    
    

    public int getCartTotals()
    {
        return
        _context.CartItems.Where(c1 => c1.cardId == Id)
            .Select(c1 => c1.Book.price * c1.quantity).Sum();
    }
    
    

    

    
    
    
    public int ReduceQuantity(Book book)
    {
        var cartItem =getCardItem(book);
        var remainingQuantity = 0;
        
        if (cartItem != null)
        {
            if (cartItem.quantity > 1)
            {
                remainingQuantity = -- cartItem.quantity;
                

            }
            else
            {
                _context.CartItems.Remove(cartItem);
            }
        }
        _context.SaveChanges();
        return remainingQuantity;
    }

    
    public int IncreaseQuantity(Book book)
    {
        var cartItem = getCardItem(book);
        var remainingQuantity = 0;

        if (cartItem != null)
        {
            if (cartItem.quantity > 0)
            {
                remainingQuantity = ++cartItem.quantity;
            }
        }
        _context.SaveChanges();

        return remainingQuantity;
    }





    public void RemoveFromCart(Book book)
    {
        var cartItem =getCardItem(book);
        if (cartItem != null)
        {
            _context.CartItems.Remove(cartItem);
        }
        _context.SaveChanges();

    }

    public void cleanCart()
    {
        var CartItems = _context.CartItems.
            Where(c1 => c1.cardId == Id);
        _context.CartItems.RemoveRange(CartItems);
        _context.SaveChanges();

    }
    
    
    
    
    
    
    

}