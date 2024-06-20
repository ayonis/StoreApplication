
using Store.Models;
using Microsoft.EntityFrameworkCore;
using StoreApplication.Interfaces;
using System.Linq;

namespace Store.Services
{
    public class CartService : ICartService<CartItem>
    {
        protected Store_DB context;

        public CartService( Store_DB contxt)
        {
            context = contxt;
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

        public short DeleteAllItems(int customerId)
        {
            var cart = context.Carts.SingleOrDefault(c => c.CustomerId == customerId);

            var cartItem = context.CartItems.Where(cart_item => cart_item.CartId == cart.Id);

            if (cartItem == null)
            {
                return -1;
            }
            else
            {
                context.CartItems.RemoveRange(cartItem);
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
         
                var cartItems = context.Carts.Where(c => c.CustomerId == customerId).Include(c => c.CartItems).ThenInclude(cartItem => cartItem.Item);
                return cartItems;
            
        }

        public List<CartItem> FindItemByCondition(Func<CartItem, bool> predicate)
        {
            return context.CartItems.Where(predicate).ToList();
        }


    }
}
