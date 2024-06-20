using Store.Services;
using Microsoft.AspNetCore.Mvc;
using Store.Interfaces;


namespace Store.Controllers
{
    public class TestController : Controller
    {
        private IBasicServices<Employee> _employeeService;
      
        public TestController(IBasicServices<Employee> employeeService)
        {
            _employeeService = employeeService;
  
        }
        public IActionResult Index()
        {
            /* var Books = _bookService.GetAllBooks();
             foreach (var Book in Books)
             {
                 Console.WriteLine($"{Book.Id} : {Book.Name} : {Book.Image} : {Book.Author}");
             }*/




          //  _employeeService.AddRecord(Item);


            return View("Home");
        }
    }
}
