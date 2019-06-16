using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.Models
{
    public class Book: Item
    {
        public string isbn { get; set; }
        public List<string> authors { get; set; }
        public int numberOfPages { get; set; }
        public string publisher { get; set; }
        public string country { get; set; }
        public string mediaType { get; set; }
        public DateTime released { get; set; }
        public string[] characters { get; set; }
        public string[] povCharacters { get; set; }
    }
}
