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
        public IActionResult GetAllLessInfo(int customerId)
        {
            List<CartItem> cartItems = _CartService.GetAllItems(customerId);

            if (cartItems == null) return BadRequest();

            else { 
                return Json(cartItems); 
            }

        }
        [HttpGet]
        public IActionResult GetAll(int customerId)
        {
            IQueryable<dynamic> cartItems = _CartService.GetAllItemsInfo(customerId);

            if (cartItems == null) return BadRequest();

            else
            {
                return Json(cartItems);
            }

        }

        [HttpPost]
        public IActionResult Create(int customerId , int itemId , int quantity)
        {
            short status = _CartService.AddItem(customerId, itemId, quantity);
            if (status == -1)
            {
                return BadRequest();
            }
            else
            {
                return NoContent();
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
                return NoContent();
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
                return NoContent();
            }
        }
    }
}
