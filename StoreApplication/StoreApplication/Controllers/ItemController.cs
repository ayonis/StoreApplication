using Microsoft.AspNetCore.Mvc;
using Store.Interfaces;
using Store;

namespace StoreApplication.Controllers
{
    public class ItemController : Controller
    {
        IBasicServices<Item> _ItemService;
        public ItemController(IBasicServices<Item> itemService)
        {
            _ItemService = itemService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(_ItemService.GetAll());

        }

        [HttpGet]
        public IActionResult GetItem(int id)
        {
            return Json(_ItemService.GetRecordById(id));
        }

        [HttpPost]
        public IActionResult Create(Item item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            _ItemService.AddRecord(item);

            return CreatedAtAction(nameof(_ItemService.GetRecordById), new { id = item.Id }, item);
        }

        [HttpPut]
        public IActionResult Update(Item item)
        {
            var status = _ItemService.UpdateRecord(item);
            if (status == -1)
            {
                return NotFound();
            }

            else return NoContent();

        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var status = _ItemService.DeleteRecord(id);
            if (status == -1)
            {
                return NotFound();
            }
            else return NoContent();
        }
    }
}
