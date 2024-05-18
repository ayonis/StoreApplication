using Store.Models;
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
        public string Email { get; set; }   
        public string City { get; set; }
        public string Country { get; set; }
        public string Username {  get; set; }
        public string Password { get; set; }    
        public string? Address { get; set; }
        public string Phone { get; set; }

        public List<Order> Orders { get; set; }
        public Cart Cart { get; set; }

    }
}
