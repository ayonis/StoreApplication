using Store.Interfaces;
using Store.Services;
using Store;
using StoreApplication.Models;
using StoreApplication.ViewModel;

namespace StoreApplication.Interfaces
{
	public interface IUserService<T1,T2>
	{
		public int AddRecord(T1 record);
		public short DeleteRecord(int id);
		public List<T2> GetAll();
		public T2 GetRecordById(int id);
		public short UpdateRecord(T1 record);
		
	}
}


