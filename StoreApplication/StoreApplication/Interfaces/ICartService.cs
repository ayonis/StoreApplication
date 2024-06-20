using Store.Models;

namespace StoreApplication.Interfaces
{
    public interface ICartService<T>
    {
         List<T> GetAllItems(int customerId);
         short AddItem(int customerId, int itemId, int quantity);
         short DeleteItem(int customerId, int itemId);
         short UpdateItem(int customerId, int itemId, int quantity);
         IQueryable<dynamic> GetAllItemsInfo(int customerId);
         List<T> FindItemByCondition(Func<T, bool> predicate);
         short CreateCartForCustomer(int customerId);
         short DeleteAllItems(int customerId);

	}
}
