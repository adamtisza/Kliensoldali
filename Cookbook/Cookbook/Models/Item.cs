using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookbook.Models
{
    public class Item
    {
        public string name { get; set; }
        public string url { get; set; }
        public int ID {
            get {
                return int.Parse(url.Substring(this.url.LastIndexOf('/') + 1));
            }
        }

        public string Details { get; set; } = "";

        public Item()
        {

        }

        public Item(string url, string name, string details = "")
        {
            this.url = url;
            this.name = name;
            this.Details = details;
        }
    }

    

   
}
