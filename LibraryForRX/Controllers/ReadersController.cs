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
    public class ReadersController : Controller
    {
        /// <summary>
        /// Методы для создания новой книги
        /// </summary>
        /// <returns></returns>
        private EFDBContext _efContext;

        public ReadersController(EFDBContext context)
        {
            _efContext = context; //Экземпляр модели из ORM
        }

        [HttpPost]
        public async Task<IActionResult> Create(Readers reader)
        {
            reader.Deleted = false;
            _efContext.Readers.Add(reader);
            await _efContext.SaveChangesAsync();
            return RedirectToAction("Readers", "Readers");
        }
        public IActionResult CreateRB()
        {
            return View();
        }
        public IActionResult Readers()
        {
            List<Readers> _listDirectories = new List<Readers>();
            _listDirectories = _efContext.Readers.Where(c => c.Deleted == false).ToList();
            return View(_listDirectories);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                Readers reader = await _efContext.Readers.FirstOrDefaultAsync(p => p.Id == id);
                reader.Books = _efContext.ManagementBooks.Where(b => b.Reader.Id == id).Select(a => a.Book).ToList<Books>();

                if (reader != null)
                    return View(reader);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Change(Readers reader)
        {
            _efContext.Readers.Update(reader);
            await _efContext.SaveChangesAsync();
            return RedirectToAction("Readers", "Readers");
        }
        public async Task<IActionResult> ChangeRB(int? id)
        {
            if (id != null)
            {
                Readers reader = await _efContext.Readers.FirstOrDefaultAsync(p => p.Id == id);
                if (reader != null)
                    return View(reader);
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Readers reader = await _efContext.Readers.FirstOrDefaultAsync(p => p.Id == id);
                if (reader != null)
                {
                    reader.Deleted = true;
                    _efContext.Readers.Update(reader);
                    await _efContext.SaveChangesAsync();
                    return RedirectToAction("Readers", "Readers");
                }
            }
            return NotFound();
        }
        //public IActionResult SearchReaders(string searchString)
        //{
        //    Readers readers = _efContext.Readers.Where(c => c.FIO.Contains(searchString)).ToList();
        //    readers.Books = _efContext.ManagementBooks.Where(b => b.Reader.Id == id).Select(a => a.Book).ToList<Books>();

        //    return View(readers);
        //}
        public async Task<IActionResult> SearchReaders(string searchString)
        {
            if (searchString != null)
            {
                var readers = _efContext.Readers.Where(c => c.FIO.Contains(searchString)).ToList();
                foreach (var item in readers)
                {
                    Readers reader = await _efContext.Readers.FirstOrDefaultAsync(p => p.FIO == item.FIO);
                    reader.Books = _efContext.ManagementBooks.Where(b => b.Reader.Id == item.Id).Select(a => a.Book).ToList<Books>();
                }
                //int idReader = _efContext.Readers.Where(c => c.FIO.Contains(searchString)).FirstOrDefault().Id;
                //Readers readMan = _efContext.Readers.FirstOrDefault(c => c.Id == 1);
                //readMan.Books = _efContext.ManagementBooks.Where(b => b.Reader.Id == 1).Select(a => a.Book).ToList<Books>();
                //Readers reader = _efContext.Readers.Where(c => c.FIO.Contains(searchString)).ToList().FirstOrDefault();
                //reader.Books = _efContext.ManagementBooks.Where(b => b.Reader.Id == idReader).Select(a => a.Book).ToList<Books>();
                //Readers reader = await _efContext.Readers.FirstOrDefaultAsync(p => p.FIO == readers.FIO);
                //reader.Books = _efContext.ManagementBooks.Where(b => b.Reader.Id == idReader).Select(a => a.Book).ToList<Books>();
                if (readers != null)
                    return View(readers);
            }
            return NotFound();
        }
    }
}
