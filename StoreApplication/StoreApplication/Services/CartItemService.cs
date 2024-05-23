using Store.Interfaces;
using Store.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Linq;

namespace Store.Services
{
    public class CartItemService : IBasicServiceExtention<CartItem>
    {
        protected Store_DB context;
        IConfiguration _configuration;

        public CartItemService(IConfiguration configuration)
        {
            context = new Store_DB(configuration);
            _configuration = configuration;
        }
        public short AddRecord(CartItem record)
        {
            if (record is null)
            {
                return -1;
            }
            else
            {

                var cart = context.Carts.AsNoTracking().SingleOrDefault(c => c.Id == record.CartId);

                var item = context.Items.AsNoTracking().SingleOrDefault(b => b.Id == record.ItemId);

                if (cart == null || item == null)
                {
                    return -1;
                }
                else
                {
                    context.CartItems.Add(record);
                    context.SaveChanges();
                    return 1;
                }
            }
        }

        public short DeleteRecord(int cartId, int itemId)
        {
            var existingRecord = GetRecordById(cartId, itemId);

            if (existingRecord == null)
            {
                return -1;
            }
            else
            {
                context.CartItems.Remove(existingRecord);
                context.SaveChanges();
                return 1;
            }
        }

        public List<CartItem> FindRecordsByCondition(Func<CartItem, bool> predicate)
        {
            return context.CartItems.Where(predicate).ToList();
        }

        public List<CartItem> GetAll(int cartId)
        {
            var cartItems = context.CartItems.AsNoTracking().Where(CIs => CIs.CartId == cartId).ToList();
            return cartItems;
        }

        public CartItem GetRecordById(int cartId, int itemId)
        {
            var cartItem = context.CartItems.AsNoTracking().SingleOrDefault(CI => CI.CartId == cartId && CI.ItemId == itemId);
            return cartItem;
        }

        public short UpdateRecord(CartItem record)
        {
            var existingRecord = GetRecordById(record.CartId, record.ItemId);

            if (record is null || existingRecord is null) return -1;


            else
            {
                context.CartItems.Update(record);
                context.SaveChanges();
                return 1;
            }
        }

       
    }
}
