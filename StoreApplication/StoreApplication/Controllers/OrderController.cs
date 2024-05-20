using Microsoft.AspNetCore.Mvc;
using Store.Interfaces;
using Store;
using Store.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Transactions;
using Microsoft.VisualBasic;

namespace StoreApplication.Controllers
{
    public class OrderController : Controller
    {
        IBasicServices<Order> _OrderService;
        IConfiguration _Configuration;
        public OrderController(IBasicServices<Order> orderervice, IConfiguration configuration)
        {
            _OrderService = orderervice;
            _Configuration = configuration;
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

        public IActionResult Create(int customerId)
        {
            OrderService orderservice = new OrderService(_Configuration);
            CartService cartService = new CartService(_Configuration);
            Order order = new Order();
            order.CustomerId = customerId;

            int orderId = orderservice.AddRecord(order);

            if(orderId == -1 ) return BadRequest(" Order Creation Fail");
           
             (int status,string msg) = orderservice.AddOrderItems(customerId, orderId);

            if (status == -1) return BadRequest(status + " " + msg);

            else
            {
                cartService.DeleteAllItems(customerId);
                return Ok("Ok: The Order Is Done Successfully");
            }
        }
    }
}
