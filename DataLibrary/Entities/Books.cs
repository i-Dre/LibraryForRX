using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Entities
{
    public class Books
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Vendor { get; set; }
        public int Year { get; set; }
        public int Copies { get; set; }
        public bool Deleted { get; set; }
        public bool Issued { get; set; }
       
    }
}
