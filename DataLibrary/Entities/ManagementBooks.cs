using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Entities
{
    public class ManagementBooks
    {
        public int Id { get; set; }
        public Books Book { get; set; }
        public Readers Reader { get; set; }
    }
}
