using DataLibrary;
using DataLibrary.Entities;
using LibraryForRX.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace LibraryForRX.Controllers
{
    public class ManagementController : Controller
    {
        private EFDBContext _efContext;

        public ManagementController(EFDBContext context)
        {
            _efContext = context;
        }




        [HttpPost]
        public async Task<IActionResult> Create(ManagementBooks managementBooks)
        {
            Readers readers = _efContext.Readers.Where(b => b.Deleted != true).FirstOrDefault(r => r.Id == managementBooks.Reader.Id);
            Books book = _efContext.Books.Where(b => b.Deleted != true).FirstOrDefault(r => r.Id == managementBooks.Book.Id);
            managementBooks.Reader = readers;
            managementBooks.Book = book;
            _efContext.ManagementBooks.Add(managementBooks);           
            await _efContext.SaveChangesAsync();
            return RedirectToAction("Management", "Management");
        }
        public IActionResult CreateMB()
        {
            ViewBag.Readers = new SelectList(_efContext.Readers.Where(b => b.Deleted != true).ToList(), "Id", "FIO");
            ViewBag.Books = new SelectList(_efContext.Books.Where(b => b.Deleted != true).ToList(), "Id", "Name");
            return View();
        }
        public IActionResult Management()
        {
            ViewBag.Readers = _efContext.Readers.Where(c => c.Deleted == false).ToList();
            ViewBag.Books = _efContext.Books.Where(c => c.Deleted == false).ToList();
            List<ManagementBooks> _listDirectories = new List<ManagementBooks>();
            _listDirectories = _efContext.ManagementBooks.ToList();
            return View(_listDirectories);
            
        }
    }
}
