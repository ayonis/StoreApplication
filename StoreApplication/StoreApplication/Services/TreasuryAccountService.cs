using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Store;
using Store.Interfaces;
using StoreApplication.Interfaces;
using StoreApplication.Models;

namespace StoreApplication.Services
{
    public class TreasuryAccountService : IBasicServiceTreasuryAccountExtention<TreasuryAccount>
	{
        protected Store_DB context;
        IBasicServices<TreasuryTransaction> _TreasuryTransactionService;
		public TreasuryAccountService(Store_DB contxt, IBasicServices<TreasuryTransaction> TreasuryTransactionService)
        {
            context = contxt;
            _TreasuryTransactionService = TreasuryTransactionService;

		}

        public int AddRecord(TreasuryAccount record)
        {
            if (record is null || record.Balance < 0)
            {
                return -1;
            }
            else
            {
                context.TreasuryAccounts.Add(record);
                context.SaveChanges();
                return 1;
            }
        }

        public short DeleteRecord(int id)
        {
            var existingRecord = GetRecordById(id);

            if (existingRecord == null)
            {
                return -1;
            }
            else
            {
                context.TreasuryAccounts.Remove(existingRecord);
                context.SaveChangesAsync();
                return 1;
            }
        }

        public List<TreasuryAccount> GetAll()
        {
            var TreasuryAccounts = context.TreasuryAccounts.ToList();
            return TreasuryAccounts;
        }

        public TreasuryAccount GetRecordById(int id)
        {

            var treasuryAccount = context.TreasuryAccounts.SingleOrDefault(t => t.Id == id);
            return treasuryAccount;
        }

        public short UpdateRecord(TreasuryAccount record)
        {
            var existingRecord = GetRecordById(record.Id);

            if (record is null || existingRecord is null) return -1;


            else
            {
                context.TreasuryAccounts.Update(record);
                context.SaveChanges();
                return 1;
            }
        }
        public List<TreasuryAccount> FindRecordsByCondition(Func<TreasuryAccount, bool> predicate)
        {
            return context.TreasuryAccounts.Where(predicate).ToList();
        }

        public short UpdateTreasuryAccountBalance(int treasuryId, int transactionId)
        {
            TreasuryAccount treasuryAccount = new TreasuryAccount();
           
            TreasuryTransaction treasuryTransaction = new TreasuryTransaction();

            treasuryAccount = GetRecordById(treasuryId);


            treasuryTransaction = _TreasuryTransactionService.GetRecordById(transactionId);

            if (treasuryAccount == null || treasuryTransaction == null)
            {
                return -1;
            }
            else
            {
                    treasuryAccount.Balance += treasuryTransaction.Amount;
                    context.SaveChanges();
                    return 1;
               
               
            }

        }
    }
}
