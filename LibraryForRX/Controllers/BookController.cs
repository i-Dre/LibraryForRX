using DataLibrary;
using DataLibrary.Entities;
using LibraryForRX.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryForRX.Controllers
{
    public class BookController : Controller
    {
        /// <summary>
        /// Методы для создания новой книги
        /// </summary>
        /// <returns></returns>
        private EFDBContext _efContext;

        public BookController(EFDBContext context)
        {
            _efContext = context;
        }
        public IActionResult Books()
        {
            List<Books> _listDirectories = new List<Books>();
            _listDirectories = _efContext.Books.Where(c => c.Deleted == false).ToList();
            return View(_listDirectories);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Books book)
        {
            book.Deleted = false;
            _efContext.Books.Add(book);
            await _efContext.SaveChangesAsync();
            return RedirectToAction("Books", "Book");
        }

        [HttpPost]
        public async Task<IActionResult> Change(Books book)
        {
            _efContext.Books.Update(book);
            await _efContext.SaveChangesAsync();
            return RedirectToAction("Books", "Book");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                Books book = await _efContext.Books.FirstOrDefaultAsync(p => p.Id == id);
                if (book != null)
                    return View(book);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {              
                Books book = await _efContext.Books.FirstOrDefaultAsync(p => p.Id == id);
                if (book != null)
                {
                    book.Deleted = true;
                    _efContext.Books.Update(book);
                    await _efContext.SaveChangesAsync();
                    return RedirectToAction("Books", "Book");
                }
            }
            return NotFound();
        }

        public IActionResult CreateB()
        {
            return View();
        }

        public IActionResult SearchBooks(string searchString)
        {
            var books = _efContext.Books.Where(c => c.Name.Contains(searchString)).ToList();
            return View(books);
        }

        public IActionResult ListOfBooksIssued()
        {
            var books = _efContext.Books.Where(c => c.Issued == true).ToList();
            return View(books);
        }

        
        public IActionResult ListOfBooksAvailable()
        {
            var books = _efContext.Books.Where(c => c.Issued == false).ToList();
            return View(books);
        }
        public async Task<IActionResult> ChangeB(int? id)
        {
            if (id != null)
            {
                Books book = await _efContext.Books.FirstOrDefaultAsync(p => p.Id == id);
                if (book != null)
                    return View(book);
            }
            return NotFound();
        }
    }
}
