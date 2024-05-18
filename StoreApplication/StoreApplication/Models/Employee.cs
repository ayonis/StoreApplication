using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SSN { get; set; }
        public string? Address { get; set; }
        public string Username {  get; set; }
        public string Password {  get; set; }
        public short Privilige { get; set; } 
        public string Phone { get; set; }
    }
}
