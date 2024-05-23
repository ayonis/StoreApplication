using Store.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Store.Services
{
    public class EmployeeService : IBasicServices<Employee>
    {
        protected Store_DB context;

        public EmployeeService(IConfiguration configuration)
        {
            context = new Store_DB(configuration);
        }

        public int AddRecord(Employee record)
        {
            if (record is null)
            {
                return -1;
            }
            else
            {
                context.Employees.Add(record);
                context.SaveChanges();
                return 1;
            }
        }

        public short DeleteRecord(int id)
        {
            var existingRecord = GetRecordById(id);

            if (existingRecord == null)
            {
                return -1;
            }
            else
            {
                context.Employees.Remove(existingRecord);
                context.SaveChangesAsync();
                return 1;
            }
        }

        public List<Employee> FindRecordsByCondition(Func<Employee, bool> predicate)
        {
            return context.Employees.Where(predicate).ToList();
        }

        public List<Employee> GetAll()
        {
            var employee = context.Employees.ToList();
            return employee;
        }

        public Employee GetRecordById(int id)
        {
            var employee = context.Employees.AsNoTracking().SingleOrDefault(e => e.Id == id);
            return employee;

        }

        public short UpdateRecord(Employee record)
        {
            var existingRecord = GetRecordById(record.Id);

            if (record is null || existingRecord is null) return -1;


            else
            {
                context.Employees.Update(record);
                context.SaveChanges();
                return 1;
            }
        }
    }
}
