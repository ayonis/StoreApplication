using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public class OrderDetail
    {
        
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        
        public Item Item { get; set; }
        public Order Order { get; set; }
    }
}
