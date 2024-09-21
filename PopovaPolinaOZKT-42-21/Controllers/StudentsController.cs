using Microsoft.AspNetCore.Mvc;

namespace PopovaPolinaOZKT_42_21.Controllers
{
    public class StudentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
