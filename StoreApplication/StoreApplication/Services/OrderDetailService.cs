using Store.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Store.Services
{
    public class OrderDetailService : IBasicServiceExtention<OrderDetail>
    {
        protected Store_DB context;
        IConfiguration _configuration;

        public OrderDetailService(IConfiguration configuration)
        {
            context = new Store_DB(configuration);
            _configuration = configuration;
        }

        public short AddRecord(OrderDetail record)
        {
            ItemService _ItemService = new ItemService(_configuration);
            OrderService _OrderService = new OrderService(_configuration);
            if (record is null)
            {
                return -1;
            }
            else
            { 
                var order = _OrderService.GetOrderInfoById(record.OrderId);
               
                var item = _ItemService.GetRecordById(record.ItemId);
                
                if (order == null || item == null || (item.Quantity - record.Quantity) < 0 )
                {
                    return -1;

                }
                else {
                    
                    context.OrderDetails.Add(record);
                    item.Quantity = item.Quantity - record.Quantity;
                    _ItemService.UpdateRecord(item);
                    context.SaveChanges();
                    return 1;
                }
            }
        }

        public short DeleteRecord(int orderId , int itemId)
        {
            var existingRecord = GetRecordById(orderId, itemId);

            if (existingRecord == null)
            {
                return -1;
            }
            else
            {
                context.OrderDetails.Remove(existingRecord);
                context.SaveChanges();
                return 1;
            }
        }

        public List<OrderDetail> GetAll(int orderId)
        {
            var OrderDetail = context.OrderDetails.AsNoTracking().Where(ods => ods.OrderId == orderId).ToList();
            return OrderDetail;
        }

        public OrderDetail GetRecordById(int orderId, int itemId)
        {
            var OrderDetail = context.OrderDetails.AsNoTracking().SingleOrDefault(od => od.OrderId == orderId && od.ItemId == itemId);
            return OrderDetail;
        }

        public short UpdateRecord( OrderDetail record)
        {
            var existingRecord = GetRecordById(record.OrderId , record.ItemId);

            if (record is null || existingRecord is null) return -1;


            else
            {
                context.OrderDetails.Update(record);
                context.SaveChanges();
                return 1;
            }
        }



        private void updateQuantityOfItem(OrderDetail existingRecord)
        {
            int quantityOfDeletedItemFromOrder = existingRecord.Quantity;
            int BookId = existingRecord.ItemId;
            ItemService bookService = new ItemService(_configuration);

            bookService.UpdateItemQuantity(BookId, quantityOfDeletedItemFromOrder);
        }

        
    }
}
