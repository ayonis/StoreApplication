namespace StoreApplication.Models
{
    public class TreasuryAccount 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }
        
        public List<TreasuryTransaction> Transactions { get; set; }
    }
}
