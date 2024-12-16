using Bookly.Data;

namespace Bookly.Models;

public class Cart
{
    private readonly BooklyContext _context;

    public Cart(BooklyContext context)
    {
        _context = context;
    }

    public string Id {get;set;}
    public List<CartItem> cardItems{get;set;}

    public static Cart getCart(IServiceProvider services)
    {
        ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
        var context = services.GetService<BooklyContext>();
        string cartId = session.GetString("Id") ?? Guid.NewGuid().ToString();
        session.SetString("Id", cartId);
        return new Cart(context){Id = cartId};
    }
    


}