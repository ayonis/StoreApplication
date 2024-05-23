using Microsoft.AspNetCore.Mvc;
using Store;
using Store.Interfaces;
using Store.Models;
using Store.Services;
using StoreApplication.Interfaces;

namespace StoreApplication.Controllers
{
    public class CartController : Controller
    {
        ICartService<CartItem> _CartService;
        public CartController(ICartService<CartItem> cartService)
        {
            _CartService = cartService;
        }
        [HttpGet]
        public IActionResult GetAllLessInfo(int Id)
        {
            List<CartItem> cartItems = _CartService.GetAllItems(Id);

            if (cartItems == null) return BadRequest();

            else { 
                return Json(cartItems); 
            }

        }
        [HttpGet]
        public IActionResult GetAll(int Id)
        {
           

            IQueryable<dynamic> cartItems = _CartService.GetAllItemsInfo(Id);

            if (cartItems == null) return BadRequest();

            else
            {
                return Json(cartItems);
            }

        }

        [HttpPost]
        public IActionResult AddItem(int customerId , int itemId , int quantity)
        {
            short status = _CartService.AddItem(customerId, itemId, quantity);
            if (status == -1)
            {
                return BadRequest("No Items Added");
            }
            else
            {
                return Ok("Item Added Successfully");
            }
        }
        [HttpDelete]
        public IActionResult Delete(int customerId, int itemId)
        {
            short status = _CartService.DeleteItem(customerId, itemId);
            if (status == -1)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }

        [HttpPut]
        public IActionResult Update(int customerId, int itemId , int quantity)
        {
            short status = _CartService.UpdateItem(customerId, itemId , quantity);
            if (status == -1)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }
    }
}
