using CarRentals.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRental_UI.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Date()
        {
            return View();
        }
    }
}
