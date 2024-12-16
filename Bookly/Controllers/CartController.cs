using Bookly.Data;
using Bookly.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookly.Controllers
{
    public class CartController : Controller
    {
        private readonly BooklyContext _context;
        
        private readonly Cart _cart;

        public CartController(Cart cart , BooklyContext context)
        {
            _cart = cart;
            _context = context;
        }

        // GET: Cart
        public ActionResult Index()
        {
            var items = _cart.getAllCardItems();
            _cart.CardItems = items;
            return View(_cart);
        }

        
        // POST: Cart
        public IActionResult AddToCart(int id)
        {
            var selectedBook= GetBookById(id);
            if (selectedBook != null)
            {
                _cart.AddToCart(selectedBook,1);
            }
            return RedirectToAction("Index","Store");
        }
        
        
        public Book GetBookById(int id)
        {
            return _context.Books.FirstOrDefault(b => b.Id == id);
        }


        public IActionResult RemoveFromCart(int id)
        {
            var selectedBook = GetBookById(id);
            if (selectedBook != null)
            {
                _cart.RemoveFromCart(selectedBook);
            }
            return RedirectToAction("Index");
            
        }
        
        
        
        public IActionResult ReduceQuantity(int id)
        {
            var selectedBook = GetBookById(id);
            if (selectedBook != null)
            {
                _cart.ReduceQuantity(selectedBook);
            }
            return RedirectToAction("Index");
            
        }

        
        public IActionResult IncreasQuantity(int id)
        {
            var selectedBook = GetBookById(id);
            if (selectedBook != null)
            {
                _cart.IncreaseQuantity(selectedBook);
            }
            return RedirectToAction("Index");
            
        }

        
        
        public IActionResult clearCart()
        {
            _cart.cleanCart();
            return RedirectToAction("Index");
            
        }
        
        
        
        
        
        
        
        
        
        
        
    }
}
