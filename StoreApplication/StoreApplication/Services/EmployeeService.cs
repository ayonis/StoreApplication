using Store.Interfaces;
using Microsoft.EntityFrameworkCore;
using StoreApplication.ViewModel;
using StoreApplication.Models;
using StoreApplication.Interfaces;

namespace Store.Services
{
    public class EmployeeService : IUserService<EmployeeViewModel, Employee>
	{
        protected Store_DB context;
        IBasicServices<ApplicationUser> _ApplicationUser;
		public EmployeeService( Store_DB contxt, IBasicServices<ApplicationUser> applicationUser)
        {
            context =  contxt;
			_ApplicationUser = applicationUser;

		}

        public int AddRecord(EmployeeViewModel record)
        {
            var user = _ApplicationUser.FindRecordsByCondition(u => u.UserName == record.UserName && u.Email != record.Email);
            if (record is null || user.Count() != 0)
            {
                return -1;
            }
            
            else
            {
                ApplicationUser employeeUser = new ApplicationUser
                {
                    UserName = record.UserName,
                    NormalizedUserName = record.NormalizedUserName,
                    Email = record.Email,
                    NormalizedEmail = record.NormalizedEmail,
                    EmailConfirmed = record.EmailConfirmed,
                    PasswordHash = record.PasswordHash,
                    SecurityStamp = record.SecurityStamp,
                    ConcurrencyStamp = record.ConcurrencyStamp,
                    PhoneNumber = record.PhoneNumber,
                    PhoneNumberConfirmed = record.PhoneNumberConfirmed,
                    TwoFactorEnabled = record.TwoFactorEnabled,
                    LockoutEnd = record.LockoutEnd,
                    LockoutEnabled = record.LockoutEnabled,
                    AccessFailedCount = record.AccessFailedCount

                };
                int userId = _ApplicationUser.AddRecord(employeeUser);

                Employee employee = new Employee
                {
                    Name = record.Name,
                    SSN = record.SSN,
                    Address = record.Address,
                    UserFK = userId

                };
                context.Employees.Add(employee);
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
                int userFk = (existingRecord.UserFK) != null ? (int)existingRecord.UserFK:-1;
               
				_ApplicationUser.DeleteRecord(userFk);
				context.Employees.Remove(existingRecord);
                context.SaveChanges();
                return 1;
            }
        }


        public List<Employee> GetAll()
        {
            var employee = context.Employees.Include(u => u.ApplicationUser).ToList();
            return employee;
        }

        public Employee GetRecordById(int id)
        {
            var employee = context.Employees.Include(u => u.ApplicationUser).AsNoTracking().SingleOrDefault(e => e.Id == id);
            return employee;

        }

        public short UpdateRecord(EmployeeViewModel record)
        {
            var existingRecord = GetRecordById(record.Id);

			int userFk = (record.UserFK) != null ? (int)existingRecord.UserFK : -1;

			var existinUser = _ApplicationUser.GetRecordById(userFk);

            if (record is null || existingRecord is null || existinUser is null || userFk != record.UserFK) 
                return -1;


            else
            {
                ApplicationUser user = new ApplicationUser
                {
                    Id = userFk,
					UserName = record.UserName,
					NormalizedUserName = record.NormalizedUserName,
					Email = record.Email,
					NormalizedEmail = record.NormalizedEmail,
					EmailConfirmed = record.EmailConfirmed,
					PasswordHash = record.PasswordHash,
					SecurityStamp = record.SecurityStamp,
					ConcurrencyStamp = record.ConcurrencyStamp,
					PhoneNumber = record.PhoneNumber,
					PhoneNumberConfirmed = record.PhoneNumberConfirmed,
					TwoFactorEnabled = record.TwoFactorEnabled,
					LockoutEnd = record.LockoutEnd,
					LockoutEnabled = record.LockoutEnabled,
					AccessFailedCount = record.AccessFailedCount
				};

				Employee employee = new Employee
				{
                    Id = record.Id,
					Name = record.Name,
					SSN = record.SSN,
					Address = record.Address,
					UserFK = userFk

				};
				_ApplicationUser.UpdateRecord(user);

                context.Employees.Update(employee);
                context.SaveChanges();
                return 1;
            }
        }

    }
}
