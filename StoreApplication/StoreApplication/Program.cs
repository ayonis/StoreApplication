using Store.Services;
using Microsoft.Extensions.DependencyInjection;
using Store.Interfaces;
using Store;
using Store.Models;
using StoreApplication.Interfaces;
using StoreApplication.Models;
using StoreApplication.Services;

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
            builder.Services.AddTransient(typeof(IBasicServices<Employee>), typeof(EmployeeService));
            builder.Services.AddTransient(typeof(ICartService<CartItem>), typeof(CartService));
            builder.Services.AddTransient(typeof(IBasicServices<Category>), typeof(CategoryService));
       
            builder.Services.AddTransient(typeof(IBasicServices<Customer>), typeof(CustomerService));
            builder.Services.AddTransient(typeof(IBasicServices<Item>), typeof(ItemService));
            builder.Services.AddTransient(typeof(IBasicServices<Order>), typeof(OrderService));
            builder.Services.AddTransient(typeof(IBasicServices<TreasuryAccount>), typeof(TreasuryAccountService));


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
