using Microsoft.EntityFrameworkCore;
using Store;
using Store.Interfaces;
using StoreApplication.Models;
using System.Linq;

namespace StoreApplication.Services
{
	public class ApplicationUserService : IBasicServices<ApplicationUser>
	{
		protected Store_DB context;
		public ApplicationUserService(Store_DB contxt)
		{ 
			context = contxt;
		}
		public int AddRecord(ApplicationUser record)
		{
			context.Users.Add(record);
			context.SaveChanges();
			var user = context.Users.FirstOrDefault(u => u.UserName == record.UserName && u.Email == record.Email);
			if(user is null)
			{
				return -1;
			}
			else
			{
				return user.Id;
			}
			
		}

		public short DeleteRecord(int id)
		{
			var user = context.Users.FirstOrDefault(u => u.Id == id);
			if(user is null)
			{
				return -1;
			}
			else { 
				context.Remove(user);
				context.SaveChanges();
				return 1;
			}
		}

		public List<ApplicationUser> FindRecordsByCondition(Func<ApplicationUser, bool> predicate)
		{
			return context.Users.Where(predicate).ToList();
		}

		public List<ApplicationUser> GetAll()
		{
			var applicationUsers = context.Users.ToList();
			return applicationUsers;
		}

		public ApplicationUser GetRecordById(int id)
		{
			var applicationUser = context.Users.AsNoTracking().FirstOrDefault(u => u.Id == id);
			return applicationUser;
		}

		public short UpdateRecord(ApplicationUser record)
		{
			var existingRecord = GetRecordById(record.Id);

			if (record is null || existingRecord is null) return -1;


			else
			{
				context.Users.Update(record);
				context.SaveChanges();
				return 1;
			}
		}
	}
}
