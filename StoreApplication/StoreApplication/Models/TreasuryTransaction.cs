using Store;
using System.ComponentModel.DataAnnotations;

namespace StoreApplication.Models
{
    public class TreasuryTransaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public double Amount { get; set; }
        public string Description { get; set; }
        
        public byte Type { get; set; }  // Debit or Credit
        public int? OrderId { get; set; }
        public int TreasuryAccountId { get; set; }
        public TreasuryAccount TreasuryAccount { get; set; }
        public Order Order { get; set; }
    }
}
