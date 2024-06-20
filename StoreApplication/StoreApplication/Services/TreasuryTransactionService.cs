using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Store;
using Store.Interfaces;
using StoreApplication.Models;

namespace StoreApplication.Services
{
    public class TreasuryTransactionService : IBasicServices<TreasuryTransaction>
    {
        protected Store_DB context;

        public TreasuryTransactionService(Store_DB contxt)
        {
            context = contxt;
        }
        public int AddRecord(TreasuryTransaction record)
        {
            if (record is null)
            {
                return -1;
            }
            else
            {
                context.TreasuryTransactions.Add(record);
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
                context.TreasuryTransactions.Remove(existingRecord);
                context.SaveChangesAsync();
                return 1;
            }
        }

        public List<TreasuryTransaction> FindRecordsByCondition(Func<TreasuryTransaction, bool> predicate)
        {
            return context.TreasuryTransactions.Where(predicate).ToList();
        }

        public List<TreasuryTransaction> GetAll()
        {
            var TreasuryTransactions = context.TreasuryTransactions.ToList();
            return TreasuryTransactions;
        }

        public TreasuryTransaction GetRecordById(int id)
        {
            var TreasuryTransaction = context.TreasuryTransactions.SingleOrDefault(t => t.Id == id);
            return TreasuryTransaction;
        }

        public short UpdateRecord(TreasuryTransaction record)
        {
            var existingRecord = GetRecordById(record.Id);

            if (record is null || existingRecord is null) return -1;


            else
            {
                context.TreasuryTransactions.Update(record);
                context.SaveChanges();
                return 1;
            }
        }




    }
}
