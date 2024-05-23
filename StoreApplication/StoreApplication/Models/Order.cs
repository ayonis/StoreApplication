using StoreApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{

    public class Order
    {
       public int Id { get; set; }
       public double Cost { get; set; }
       public double FinalCost { get; set; }
       public float Discount { get; set; }
       public DateTime DateOfOrder { get; set; }
       public float Tax {  get; set; }
       public int CustomerId { get; set; }
      
       public List<Item> Items { get; set; }
       public Customer? Customer { get; set; }
       public List<OrderDetail> OrderDetails { get; set; }
       public List<TreasuryTransaction> TreasuryTransactions { get; set;}

    }
}
