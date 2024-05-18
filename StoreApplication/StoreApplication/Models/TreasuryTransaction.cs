using System.ComponentModel.DataAnnotations;

namespace StoreApplication.Models
{
    public class TreasuryTransaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string Type { get; set; } // Debit or Credit
    }
}
