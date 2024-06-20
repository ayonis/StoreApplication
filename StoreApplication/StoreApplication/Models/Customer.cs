using Store.Models;
using StoreApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public class Customer
    {
       
        public int Id {get; set;}
       
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string? Address { get; set; }
		[ForeignKey("ApplicationUser")]
		public int UserFK { get; set; }
		public ApplicationUser ApplicationUser { get; set; }
		public List<Order> Orders { get; set; }
        public Cart Cart { get; set; }

    }
}
