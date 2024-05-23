using Microsoft.AspNetCore.Mvc;
using Store.Interfaces;
using Store;
using Store.Services;

namespace StoreApplication.Controllers
{
    public class CustomerController : Controller
    {
        IBasicServices<Customer> _CustomerService;
        public CustomerController(IBasicServices<Customer> customerService)
        {
            _CustomerService = customerService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(_CustomerService.GetAll());

        }

        [HttpGet]
        public IActionResult GetCustomer(int id)
        {
            return Json(_CustomerService.GetRecordById(id));
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }
            _CustomerService.AddRecord(customer);
           

            return CreatedAtAction(nameof(_CustomerService.GetRecordById), new { id = customer.Id }, customer);
        }

        [HttpPut]
        public IActionResult Update(Customer customer)
        {
            var status = _CustomerService.UpdateRecord(customer);
            if (status == -1)
            {
                return NotFound();
            }

            else return Ok();

        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var status = _CustomerService.DeleteRecord(id);
            if (status == -1)
            {
                return NotFound();
            }
            else return Ok();
        }

    }
}
