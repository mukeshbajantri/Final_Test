using CarRentals.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;

namespace CarRental_UI.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            if(TempData["Login"]==null)
            {
                TempData["Login"] = false;
            }
            IEnumerable<CarDetail> carDetailList = null;

            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7161/api/");
                
                var responseTask = client.GetAsync("CarDetails");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<CarDetail>>();
                    readTask.Wait();

                    carDetailList = readTask.Result;
                }
                else
                {
                    carDetailList = Enumerable.Empty<CarDetail>();

                    ModelState.AddModelError(string.Empty, "Server error.");
                }
            }
            return View(carDetailList);

           
        }

        public IActionResult BookingCar(int CarId)
        {
            return RedirectToAction("Date", "Book");
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            TempData["Login"] = false;
            return RedirectToAction("Index");
        }
    }
}
