using Store.Interfaces;
using Microsoft.EntityFrameworkCore;
using Store.Models;
using Microsoft.Extensions.Configuration;
using StoreApplication.Interfaces;
namespace Store.Services
{
    public class CustomerService : IBasicServices<Customer>
    {
        protected Store_DB context;
        protected IConfiguration _Configuration;
        
        public CustomerService(IConfiguration configuration) 
        { 
            context = new Store_DB(configuration);
            _Configuration = configuration;
        }

        public  int AddRecord(Customer record)
        {
            if (record is null)
            {
                return -1;
            }
            else
            {
                context.Customers.Add(record);
                context.SaveChanges();
                CartService _CartService = new CartService(_Configuration);
                _CartService.CreateCartForCustomer(record.Id);

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
                context.Customers.Remove(existingRecord);
                context.SaveChangesAsync();
                return 1;
            }
        }

        public List<Customer> GetAll()
        {
            var Customers = context.Customers.ToList();
            return Customers;
        }

        public Customer GetRecordById(int id)
        {
            var Customer = context.Customers.AsNoTracking().SingleOrDefault(c => c.Id == id);
            return Customer;
        }

        public short UpdateRecord( Customer record)
        {
            var existingRecord = GetRecordById(record.Id);

            if (record is null || existingRecord is null) return -1;


            else
            {
                context.Customers.Update(record);
                context.SaveChanges();
                return 1;
            }
        }
    }
}
