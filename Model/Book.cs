using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Book : Material
    {
        public string Author { get; set; }
        public int Pages { get; set; }
        public string Format { get; set; }
        public int Year { get; set; }
    }
}
