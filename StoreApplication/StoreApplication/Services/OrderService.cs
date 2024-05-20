using Store.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Store.Services
{
    public class OrderService : IBasicServices<Order>
    {
        protected Store_DB context;
        protected IConfiguration _Configuration;

        public OrderService(IConfiguration configuration)
        {
            context = new Store_DB(configuration);
            _Configuration = configuration;
        }
        public int AddRecord(Order record)
        {
            if (record is null)
            {
                return -1;
            }
            else
            {
                CustomerService _customerService = new CustomerService(_Configuration);
                var customer = _customerService.GetRecordById(record.CustomerId);
                if (customer == null)
                {
                    return -1;
                }
                else { 
                    context.Orders.Add(record);
                    context.SaveChanges();
                    return record.Id;
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
            var Orders = context.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Item).ToList();
            return Orders;
        }

        public Order GetRecordById(int id)
        {
            var Order = context.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Item).SingleOrDefault(o => o.Id == id);
            return Order;
        }

        public Order GetOrderInfoById(int id)
        {
            var Order = context.Orders.SingleOrDefault(o => o.Id == id);
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

        public (int, string) AddOrderItems(int customerId, int orderId)
        {
            CartService cartService = new CartService(_Configuration);
            OrderDetailService orderDetailService = new OrderDetailService(_Configuration);
            OrderDetail orderDetail = new OrderDetail();

            int status = 1;
            var order = context.Orders.Find(orderId);

            var itemsInCart = cartService.GetAllItems(customerId);

            using var transaction = context.Database.BeginTransaction();
            try
            {
                foreach (var item in itemsInCart)
                {
                    orderDetail.OrderId = orderId;
                    orderDetail.ItemId = item.ItemId;
                    orderDetail.Quantity = item.Quantity;

                    status = orderDetailService.AddRecord(orderDetail);
                    if (status == -1) throw new Exception();

                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                DeleteRecord(order.Id);
                return (-1, "Faild to Save All Items : Item May Not Exist or the Quantity may be not Enough");

            }

            order.Cost = calculateOrderCost(order);
            context.SaveChanges();
            return (1, "Saved All Items");

        }

        protected double calculateOrderCost(Order order)
        {
            double cost = 0.0;
            double itemPrice = 0.0;

            ItemService itemService = new ItemService(_Configuration);
            OrderDetailService orderDetailService = new OrderDetailService(_Configuration);

            var OrderItems = context.OrderDetails.Where(orderdetail => orderdetail.OrderId == order.Id).Select(od => new { ItemId = od.ItemId, Quantity = od.Quantity });

            List<Item> items = itemService.GetAll();

            foreach (var orderitem in OrderItems)
            {
                var item = items.FirstOrDefault(i => i.Id == orderitem.ItemId);

                cost += item.Price * orderitem.Quantity;

            }

            return cost;
        }
    }
}
