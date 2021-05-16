using DataLibrary;
using DataLibrary.Entities;
using LibraryForRX.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryForRX.Controllers
{
    public class HomeController : Controller
    {
        private EFDBContext _efContext;
        
        public HomeController(EFDBContext context)
        {
            _efContext = context;
            
        }

        public IActionResult Index()
        {
            List<Books> _listDirectories = new List<Books>();
            _listDirectories = _efContext.Books.Where(c => c.Deleted==false).ToList();
            return View(_listDirectories);
        }

        
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
