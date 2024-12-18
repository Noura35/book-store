using Microsoft.AspNetCore.Mvc;


using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Bookly.Data;
using Bookly.Models;
using Microsoft.AspNetCore.Authorization;



namespace BookStore.Models
{
    [AllowAnonymous]
    public class StoreController : Controller
    {
        
        private readonly BooklyContext _context;

        public StoreController(BooklyContext context)
        {
            _context = context;
        }

        
        [AllowAnonymous]
        // GET: Store
        public async Task<IActionResult> Index()
        {
            return View(await _context.Books.ToListAsync());
        }

        
        // GET: Store/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        
      
    }
}