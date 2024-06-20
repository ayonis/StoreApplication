using Store;
using Store.Interfaces;

namespace StoreApplication.Interfaces
{
	public interface IBasicServiceOrderExtention<Order>:IBasicServices<Order>
	{
		public Order GetOrderInfoById(int id);
	}
}
