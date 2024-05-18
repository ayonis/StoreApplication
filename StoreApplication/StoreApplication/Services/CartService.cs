
using Store.Models;
using Microsoft.EntityFrameworkCore;
using StoreApplication.Interfaces;

namespace Store.Services
{
    public class CartService : ICartService<CartItem>
    {
        protected Store_DB context;

        public CartService(IConfiguration configuration)
        {
            context = new Store_DB(configuration);
        }

        public List<CartItem> GetAllItems(int customerId)
        {
             var cart = context.Carts.AsNoTracking().SingleOrDefault(c => c.CustomerId == customerId);

            if (cart == null) return null;

            else { 
                var cartItems = context.CartItems.Where(CIs => CIs.CartId == cart.Id).ToList();
                return cartItems;
        }
        }


        public short AddItem(int customerId, int itemId, int quantity)
        {
            
          var cart = context.Carts.AsNoTracking().SingleOrDefault(c => c.CustomerId == customerId);

          var item = context.Items.AsNoTracking().SingleOrDefault(i => i.Id == itemId);

                if (cart == null || item == null)
                {
                    return -1;
                }
                else
                {
                CartItem cartItem = new CartItem { CartId = cart.Id , ItemId = itemId , Quantity = quantity };
                    context.CartItems.Add(cartItem);
                    context.SaveChanges();
                    return 1;
                }
            
        }

        public short DeleteItem(int customerId, int itemId)
        {
            var cart = context.Carts.AsNoTracking().SingleOrDefault( c => c.CustomerId == customerId);

            var cartItem = context.CartItems.SingleOrDefault(cart_item => cart_item.CartId == cart.Id && cart_item.ItemId == itemId);

            if (cartItem == null)
            {
                return -1;
            }
            else
            {
                context.CartItems.Remove(cartItem);
                context.SaveChanges();
                return 1;
            }
        }

        public short UpdateItem(int customerId, int itemId, int quantity)
        {
            var cart = context.Carts.SingleOrDefault(c => c.CustomerId == customerId);

            var cartItem = context.CartItems.SingleOrDefault(cart_item => cart_item.CartId == cart.Id && cart_item.ItemId == itemId);

            if (cartItem is null) return -1;

            else
            {
                cartItem.CartId = cart.Id;
                cartItem.ItemId = itemId;
                cartItem.Quantity = quantity;
                context.SaveChanges();
                return 1;
            }
        }
        public short CreateCartForCustomer(int customerId)
        {
            try { 
           context.Carts.Add(new Cart() { CustomerId = customerId });
                context.SaveChanges();
                return 1;
            }
            catch( Exception ex)
            {
                return -1;
            }

        }

        public IQueryable<dynamic> GetAllItemsInfo(int customerId)
        {
            var cart = context.Carts.SingleOrDefault(c => c.CustomerId == customerId);

            if (cart == null) return null;

            else
            {
                var cartItems = from item in context.Items
                                join cartItem in context.CartItems
                                on item.Id equals cartItem.ItemId
                                select new { item.Id , item.Name,item.Description,item.Image , item.Price,item.Author ,item.Type,item.CategoryId , cartItem.Quantity, cartItem.CartId};
                return cartItems;
            }
        }
        /*
        public short DeleteRecord(int customerId)
        {
            var existingRecord = GetRecordById(customerId);

            if (existingRecord == null)
            {
                return -1;
            }
            else
            {
                context.Carts.Remove(existingRecord);
                context.SaveChanges();
                return 1;
            }
        }

        public List<Cart> GetAll()
        {
            var Carts = context.Carts.ToList();
            return Carts;
        }

        public Cart GetRecordById(int customerId)
        {
            var Cart = context.Carts.AsNoTracking().SingleOrDefault(c => c.CustomerId == customerId);
            return Cart;
        }

        public short UpdateRecord(Cart record)
        {
            var existingRecord = GetRecordById(record.CustomerId);

            if (record is null || existingRecord is null) return -1;


            else
            {
                context.Carts.Update(record);
                context.SaveChanges();
                return 1;
            }
        }*/

    }
}
