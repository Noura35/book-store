using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bookly.Data;
using Bookly.Models;
using Microsoft.AspNetCore.Authorization;

namespace Bookly.Controllers
{
    
    [Authorize(Roles = "Admin")]
    public class BookController : Controller
    {
        private readonly BooklyContext _context;

        public BookController(BooklyContext context)
        {
            _context = context;
        }

        
        [AllowAnonymous]
        // GET: Book
        public async Task<IActionResult> Index()
        {
            return View(await _context.Books.ToListAsync());
        }

        // GET: Book/Details/5
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

        // GET: Book/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
    [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create([Bind("Id,title,desc,lang,isbn,datePub,price,author")] Book book, IFormFile urlImg)
{
    if (ModelState.IsValid)
    {
        if (urlImg != null && urlImg.Length > 0)
        {
            // Définir le chemin de l'image où elle sera stockée
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", urlImg.FileName);

            // Sauvegarder l'image dans ce chemin
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await urlImg.CopyToAsync(stream);
            }

            // Enregistrer le chemin de l'image dans le modèle Book
            book.urlImg = "/uploads/" + urlImg.FileName;
        }

        _context.Add(book);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    return View(book);
}

// GET: Book/Edit/5
public async Task<IActionResult> Edit(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var book = await _context.Books.FindAsync(id);
    if (book == null)
    {
        return NotFound();
    }
    return View(book);
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, [Bind("Id,title,desc,lang,isbn,datePub,price,author")] Book book, IFormFile urlImg)
{
    if (id != book.Id)
    {
        return NotFound();
    }

    if (ModelState.IsValid)
    {
        try
        {
            if (urlImg != null && urlImg.Length > 0)
            {
                // Définir le chemin de l'image où elle sera stockée
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", urlImg.FileName);

                // Sauvegarder l'image dans ce chemin
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await urlImg.CopyToAsync(stream);
                }

                // Mettre à jour le chemin de l'image dans le modèle Book
                book.urlImg = "/uploads/" + urlImg.FileName;
            }

            _context.Update(book);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookExists(book.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return RedirectToAction(nameof(Index));
    }
    return View(book);
}



        // GET: Book/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
