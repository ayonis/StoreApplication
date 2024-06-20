using Microsoft.AspNetCore.Mvc;
using Store;
using Store.Interfaces;
using Store.Models;
using Store.Services;
using StoreApplication.Interfaces;
using StoreApplication.ViewModel;

namespace StoreApplication.Controllers
{
    public class EmployeeController : Controller
    {
		IUserService<EmployeeViewModel, Employee> _EmployeeService;
        public EmployeeController(IUserService<EmployeeViewModel, Employee> employeeService)
        {
            _EmployeeService = employeeService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(_EmployeeService.GetAll());

        }

        [HttpGet]
        public IActionResult GetEmployee(int id)
        {
            return Json(_EmployeeService.GetRecordById(id));
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }
            _EmployeeService.AddRecord(employee);

            return CreatedAtAction(nameof(_EmployeeService.GetRecordById), new { id = employee.Id }, employee);
        }

        [HttpPut]
        public IActionResult Update(EmployeeViewModel employee)
        {
            var status = _EmployeeService.UpdateRecord(employee);
            if (status == -1)
            {
                return NotFound();
            }

            else return NoContent();

        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var status = _EmployeeService.DeleteRecord(id);
            if (status == -1)
            {
                return NotFound();
            }
            else return NoContent();
        }

    }
}
