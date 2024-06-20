using Store.Interfaces;
using Microsoft.EntityFrameworkCore;
using Store.Models;
using Microsoft.Extensions.Configuration;
using StoreApplication.Interfaces;
using System.Linq;
using StoreApplication.ViewModel;
using StoreApplication.Models;

namespace Store.Services
{
    public class CustomerService : IUserService<CustomerViewModel, Customer>
	{
        protected Store_DB context;
		ICartService<CartItem> _CartService;
		IBasicServices<ApplicationUser> _ApplicationUser;
		public CustomerService(Store_DB contxt, ICartService<CartItem> CartService, IBasicServices<ApplicationUser> applicationUser) 
        { 
            context = contxt;
            _CartService = CartService;
			_ApplicationUser = applicationUser;
		}

        public  int AddRecord(CustomerViewModel record)
        {
			var user = _ApplicationUser.FindRecordsByCondition(u => u.UserName == record.UserName && u.Email != record.Email);
			if (record is null || user.Count() != 0)
			{
				return -1;
			}
			else
			{
				ApplicationUser customerUser = new ApplicationUser
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
				int userId = _ApplicationUser.AddRecord(customerUser);

				Customer customer = new Customer
				{
					Name = record.Name,
					City = record.City,
		            Country = record.Country,
					Address = record.Address,
					UserFK = userId

				};
				context.Customers.Add(customer);
                context.SaveChanges();
                _CartService.CreateCartForCustomer(customer.Id);

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
				int userFk = existingRecord.UserFK;

				_ApplicationUser.DeleteRecord(userFk);
				context.Customers.Remove(existingRecord);
                context.SaveChangesAsync();
                return 1;
            }
        }

        public List<Customer> GetAll()
        {
            var Customers = context.Customers.Include(u => u.ApplicationUser).ToList();
            return Customers;
        }

        public Customer GetRecordById(int id)
        {
            var Customer = context.Customers.Include(u => u.ApplicationUser).AsNoTracking().FirstOrDefault(c => c.Id == id);
            return Customer;
        }

        public short UpdateRecord(CustomerViewModel record)
        {
			var existingRecord = GetRecordById(record.Id);

			int userFk = existingRecord.UserFK;

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

				Customer customer = new Customer
				{
					Id = record.Id,
					Name = record.Name,
					City = record.City,
					Country = record.Country,
					Address = record.Address,
					UserFK = userFk

				};
				_ApplicationUser.UpdateRecord(user);
				context.Customers.Update(customer);
                context.SaveChanges();
                return 1;
            }
        }
		public List<Customer> FindRecordsByCondition(Func<Customer, bool> predicate)
		{
			return context.Customers.Where(predicate).ToList();
		}
	}
}
