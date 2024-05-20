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



            Employee Item = new Employee { 
                                    Name = "Hassan Hassan" 
                                   , Password ="1234" 
                                   , Address = "Naser City, Cairo"
                                   , Privilige = 1 
                                   ,Phone ="0102563987"
                                   ,SSN ="12345678963214"
                                   , Username = "aALy"
            };


          //  _employeeService.AddRecord(Item);


            return View("Home");
        }
    }
}
