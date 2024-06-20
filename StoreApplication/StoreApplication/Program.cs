using Store.Services;
using Microsoft.Extensions.DependencyInjection;
using Store.Interfaces;
using Store;
using Store.Models;
using StoreApplication.Interfaces;
using StoreApplication.Models;
using StoreApplication.Services;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using StoreApplication.ViewModel;

namespace BookStore
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers().AddJsonOptions(options =>{
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            });
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped(typeof(IUserService<EmployeeViewModel, Employee>), typeof(EmployeeService));
            builder.Services.AddScoped(typeof(ICartService<CartItem>), typeof(CartService));
            builder.Services.AddScoped(typeof(IBasicServices<Category>), typeof(CategoryService));

			builder.Services.AddScoped(typeof(IUserService<CustomerViewModel, Customer>), typeof(CustomerService));
			builder.Services.AddScoped(typeof(IBasicServices<Item>), typeof(ItemService));
			builder.Services.AddScoped(typeof(IBasicServiceOrderExtention<Order>), typeof(OrderService));
			builder.Services.AddScoped(typeof(IBasicServiceTreasuryAccountExtention<TreasuryAccount>), typeof(TreasuryAccountService));
			builder.Services.AddScoped(typeof(IBasicServices<TreasuryTransaction>), typeof(TreasuryTransactionService));
			builder.Services.AddScoped(typeof(IBasicServiceExtention<OrderDetail>), typeof(OrderDetailService));
			builder.Services.AddScoped(typeof(IBasicServices<ApplicationUser>), typeof(ApplicationUserService));

			builder.Services.AddDbContext<Store_DB>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            
            app.Run();
        }


    }
}
