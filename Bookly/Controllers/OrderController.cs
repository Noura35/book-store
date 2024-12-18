using Bookly.Data;
using Bookly.Models;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace Bookly.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        
        private readonly BooklyContext _context;
        private readonly Cart _cart;


        public OrderController(BooklyContext context, Cart cart)
        {
            _context = context;
            _cart = cart;
        }
        
        
        // GET: OrderController
        public ActionResult checkout()
        {
            return View("checkout");
        }




        [HttpPost]
        public IActionResult checkout(Order order)
        {
            var cartItems = _cart.getAllCardItems();
            _cart.CardItems = cartItems;
            if (_cart.CardItems.Count == 0)
            {
                ModelState.AddModelError("","Card is empty,please add first book ! ");
            }

            if (ModelState.IsValid)
            {
                createOrder(order);
                _cart.cleanCart();
                return View("CheckoutComplete",order);
            }

            return View(order);
        }


        public IActionResult CheckoutComplete(Order order)
        {
            return View(order);
        }
        
        
        
        
        
        
        
        
        
        
        public void createOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;
            var cartItems = _cart.CardItems;

            foreach (var item in cartItems)
            {
                var orderItem = new OrderItem()
                {
                    quantity = item.quantity,
                    bookId = item.Book.Id,
                    orderId = order.Id,
                    price = item.Book.price * item.quantity
                    
                };
                order.orderItems.Add(orderItem);
                order.OrderTotal += orderItem.price;
            }
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
        
        
        
        
        
        
    }
}
