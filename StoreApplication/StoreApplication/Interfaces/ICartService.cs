using Store.Models;

namespace StoreApplication.Interfaces
{
    public interface ICartService<T>
    {
        public List<T> GetAllItems(int customerId);
        public short AddItem(int customerId, int itemId, int quantity);
        public short DeleteItem(int customerId, int itemId);
        public short UpdateItem(int customerId, int itemId, int quantity);
        public IQueryable<dynamic> GetAllItemsInfo(int customerId);
        public List<T> FindItemByCondition(Func<T, bool> predicate);
    }
}
