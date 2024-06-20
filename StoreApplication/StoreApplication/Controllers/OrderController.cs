using Microsoft.AspNetCore.Mvc;
using Store.Interfaces;
using Store;
using Store.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Transactions;
using Microsoft.VisualBasic;
using StoreApplication.Interfaces;
using Store.Models;

namespace StoreApplication.Controllers
{
    public class OrderController : Controller
    {
		IBasicServiceOrderExtention<Order> _OrderService;
        ICartService<CartItem> _CartService;
	

        public OrderController( IBasicServiceOrderExtention<Order> OrderService , ICartService<CartItem> CartService)
        {
            _OrderService = OrderService;
            _CartService = CartService;

		}

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(_OrderService.GetAll());

        }
        [HttpGet]
        public IActionResult GetOrder(int Id)
        {
            return Json(_OrderService.GetRecordById(Id));

        }
        [HttpPost]
        public IActionResult Create(int customerId)
        {
            
            Order order = new Order();
            order.CustomerId = customerId;

            int orderId = _OrderService.AddRecord(order);

            if(orderId == -1 ) return BadRequest(" Order Creation Fail");

            else
            {
				_CartService.DeleteAllItems(customerId);
                return Ok("Ok: The Order Is Done Successfully");
            }
        }
        [HttpDelete]
        public IActionResult Delete(int orderId)
        {
          

            int status = _OrderService.DeleteRecord(orderId);

            if (orderId == -1) return BadRequest(" Order Creation Fail");
            else
                return Ok("Order Deleted");
            
        }
    }
}
