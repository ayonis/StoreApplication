using Store.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Store.Services
{
    public class OrderService : IBasicServices<Order>
    {
        protected Store_DB context;

        public OrderService(IConfiguration configuration)
        {
            context = new Store_DB(configuration);
        }
        public short AddRecord(Order record)
        {
            if (record is null)
            {
                return -1;
            }
            else
            {
                var customer = GetRecordById(record.CustomerId);
                if (customer == null)
                {
                    return -1;
                }
                else { 
                    context.Orders.Add(record);
                    context.SaveChanges();
                    return 1;
                }
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
                context.Orders.Remove(existingRecord);
                context.SaveChanges();
                return 1;
            }
        }

        public List<Order> GetAll()
        {
            var Orders = context.Orders.ToList();
            return Orders;
        }

        public Order GetRecordById(int id)
        {
            var Order = context.Orders.AsNoTracking().SingleOrDefault(b => b.Id == id);
            return Order;
        }

        public short UpdateRecord(Order record)
        {
            var existingRecord = GetRecordById(record.Id);

            if (record is null || existingRecord is null) return -1;
            
            else
            {
                context.Orders.Add(record);
                context.SaveChanges();
                return 1;
            }
        }
    }
}
