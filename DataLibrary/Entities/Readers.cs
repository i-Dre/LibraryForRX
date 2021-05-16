using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLibrary.Entities
{
    public class Readers
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        
        [Column(TypeName = "date")]
        public DateTime DOB { get; set; }
        public bool Deleted { get; set; }
        [NotMapped]
        public List<Books> Books { get; set; }
    }
}
