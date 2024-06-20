using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StoreApplication.Models;

namespace Store
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SSN { get; set; }
        public string? Address { get; set; }

		
		public int? UserFK { get; set; }
		public ApplicationUser? ApplicationUser { get; set; }
    }
}
