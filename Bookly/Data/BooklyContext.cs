using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bookly.Models;

namespace Bookly.Data
{
    public class BooklyContext : DbContext
    {
        public BooklyContext (DbContextOptions<BooklyContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Bookly.Models.Cart> Cart { get; set; } = default!;

        
        
        
    }
}
