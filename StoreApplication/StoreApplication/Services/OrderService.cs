using Store.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using StoreApplication.Models;
using StoreApplication.Services;
using Microsoft.VisualBasic;

namespace Store.Services
{
    public class OrderService : IBasicServices<Order>
    {
        protected Store_DB context;
        protected IConfiguration _Configuration;
        private TreasuryTransactionService treasuryTransactionService;
        private TreasuryAccountService treasuryAccountService;
        TreasuryAccount treasuryAccount;
        
        public OrderService(IConfiguration configuration)
        {
            context = new Store_DB(configuration);
            _Configuration = configuration;
            treasuryTransactionService = new TreasuryTransactionService(_Configuration);
            treasuryAccountService = new TreasuryAccountService(_Configuration);
            treasuryAccount = treasuryAccountService.GetAll().FirstOrDefault();
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
                    
                    //Cretae the Transaction First Then Create the Order
                    context.Orders.Add(record);
                    context.SaveChanges();
                    //After Creating the Order you can add its items
                    short status = AddOrderItems(record);
                    
                    if(status == 1) { 
                   int treasuryTransactionId = CreateOrderTransaction(treasuryAccount.Id, record, 1);

                    treasuryAccountService.UpdateTreasuryAccountBalance(treasuryAccount.Id, treasuryTransactionId);
                    
                    return record.Id;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }

        #region
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
                int transId = CreateOrderTransaction(treasuryAccount.Id, existingRecord, 0);
                treasuryAccountService.UpdateTreasuryAccountBalance(treasuryAccount.Id, transId);

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
                context.Orders.Update(record);
                context.SaveChanges();
                return 1;
            }
        }

        public List<Order> FindRecordsByCondition(Func<Order, bool> predicate)
        {
            return context.Orders.Where(predicate).ToList();
        }

        public short AddOrderItems(Order order)
        {
            
            CartService cartService = new CartService(_Configuration);
            OrderDetailService orderDetailService = new OrderDetailService(_Configuration);

            OrderDetail orderDetail = new OrderDetail();

            int status = 1;

            var itemsInCart = cartService.GetAllItems(order.CustomerId);

            using var transaction = context.Database.BeginTransaction();
            
            try
            {
                if (itemsInCart == null) throw new Exception();

                foreach (var item in itemsInCart)
                {
                    orderDetail.OrderId = order.Id;
                    orderDetail.ItemId = item.ItemId;
                    orderDetail.Quantity = item.Quantity;

                    status = orderDetailService.AddRecord(orderDetail);
                    if (status == -1) throw new Exception();

                }

                order.Cost = calculateOrderCost(order);
                
                context.SaveChanges();

                transaction.Commit();

            }
            catch (Exception ex)
            {
                transaction.Rollback();
                DeleteRecord(order.Id);
                return -1;

            }


            return 1;

        }
        #endregion
        protected int CreateOrderTransaction(int treasuryAccountId , Order order, byte TransactionType)
        {
            TreasuryTransaction treasuryTransaction = new TreasuryTransaction();

            if(TransactionType == 1) { 
                treasuryTransaction.Date = DateTime.Now;
                treasuryTransaction.Type = TransactionType;
                treasuryTransaction.Amount = order.Cost;
                treasuryTransaction.Description = "Adding the Transaction Of Order Number = " + order.Id;
                treasuryTransaction.OrderId = order.Id;
                treasuryTransaction.TreasuryAccountId = treasuryAccountId;

                int status = treasuryTransactionService.AddRecord(treasuryTransaction);

                if (status == -1) return -1;
                else
                {

                    return treasuryTransaction.Id;
                }
            }
            else
            {
                treasuryTransaction.Date = DateTime.Now;
                treasuryTransaction.Type = TransactionType;
                treasuryTransaction.Amount = -(order.Cost);
                treasuryTransaction.Description = "Deleting the Order Number = " + order.Id;
                treasuryTransaction.OrderId = null;
                treasuryTransaction.TreasuryAccountId = treasuryAccountId;

                int status = treasuryTransactionService.AddRecord(treasuryTransaction);

                if (status == -1) return -1;
                else
                {

                    return treasuryTransaction.Id;
                }
            }


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
