using Store;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Store.Interfaces
{
    public interface IBasicServices<T>
    {

        List<T> GetAll();
        T GetRecordById(int id);
        short AddRecord(T record);
        short UpdateRecord(T record);
        short DeleteRecord(int id);
    }
}


