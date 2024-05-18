using Microsoft.AspNetCore.Mvc;
using Store;
using Store.Interfaces;
using Store.Models;
using Store.Services;

namespace StoreApplication.Controllers
{
    public class EmployeeController : Controller
    {
        IBasicServices<Employee> _EmployeeService;
        public EmployeeController(IBasicServices<Employee> employeeService)
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
        public IActionResult Create(Employee employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }
            _EmployeeService.AddRecord(employee);

            return CreatedAtAction(nameof(_EmployeeService.GetRecordById), new { id = employee.Id }, employee);
        }

        [HttpPut]
        public IActionResult Update(Employee employee)
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
