using Microsoft.AspNetCore.Mvc;
using Store.Interfaces;
using Store.Models;
using Store.Services;

namespace StoreApplication.Controllers
{
    public class CategoriesController : Controller
    {
        IBasicServices<Category> _CategoryService;
        public CategoriesController(IBasicServices<Category> CategoryService)
        {
            _CategoryService = CategoryService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(_CategoryService.GetAll());

        }
        [HttpGet]
        public IActionResult GetCategory(int id)
        {
            return Json(_CategoryService.GetRecordById(id));
        }


        [HttpPost]
        public IActionResult Create( Category category)
        {
            if (category == null)
            {
                return BadRequest();
            }
            _CategoryService.AddRecord(category);
            return CreatedAtAction(nameof(_CategoryService.GetRecordById), new { id = category.Id }, category);
        }

        [HttpPut]
        public IActionResult Update(Category category)
        {
            var status = _CategoryService.UpdateRecord(category);
            if (status == -1)
            {
                return NotFound();
            }

            else return NoContent();

        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var status = _CategoryService.DeleteRecord(id);
            if (status == -1)
            {
                return NotFound();
            }
            else return NoContent();
        }


    }
}
