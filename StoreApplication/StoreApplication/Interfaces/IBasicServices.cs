using Store;
using StoreApplication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Store.Interfaces
{
    public interface IBasicServices<T>
    {

        List<T> GetAll();
        T GetRecordById(int id);
        int AddRecord(T record);
        short UpdateRecord(T record);
        short DeleteRecord(int id);
        public List<T> FindRecordsByCondition(Func<T, bool> predicate);
    }
}


