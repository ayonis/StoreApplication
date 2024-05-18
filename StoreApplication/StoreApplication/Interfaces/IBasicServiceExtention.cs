namespace Store.Interfaces
{
    public interface IBasicServiceExtention<T> 
    {
        List<T> GetAll(int Id);
        T GetRecordById(int Id_Pk1, int Id_Pk2);
        short AddRecord(T record);
        short UpdateRecord(T record);
        short DeleteRecord(int Id_Pk1, int Id_Pk2);
    }
}
