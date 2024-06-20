using Store.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using StoreApplication.Interfaces;

namespace Store.Services
{
    public class OrderDetailService : IBasicServiceExtention<OrderDetail>
    {
        protected Store_DB context;
      
        IBasicServices<Item> _ItemService;
		

		public OrderDetailService(Store_DB contxt , IBasicServices<Item> ItemService )
        {
            context =  contxt;
            _ItemService = ItemService;
       
		}

        public short AddRecord(OrderDetail record)
        {
          
            if (record is null)
            {
                return -1;
            }
            else
            { 
               
               
                var item = _ItemService.GetRecordById(record.ItemId);
                
                if ( item == null || (item.Quantity - record.Quantity) < 0 )
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


        public List<OrderDetail> FindRecordsByCondition(Func<OrderDetail, bool> predicate)
        {
            return context.OrderDetails.Where(predicate).ToList();
        }


    }
}
