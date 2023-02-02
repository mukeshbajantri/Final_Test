
using System.Text;
using System.Text.Json;
using CarRentals.Models;
using Microsoft.AspNetCore.Mvc;


namespace CarRental_UI.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            
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

                    ModelState.AddModelError(string.Empty, "Server error..");
                }
            }
            return View(carDetailList);
        }

        [HttpGet]
        public IActionResult Edit(int CarId)
        {
            using (var client = new HttpClient())
            {
                CarDetail carDetail = new CarDetail();
                client.BaseAddress = new Uri("https://localhost:7161/api/");
                //HTTP GET
                var responseTask = client.GetAsync("CarDetails/" + CarId);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<CarDetail>();
                    readTask.Wait();
                    carDetail = readTask.Result;

                    if (carDetail != null)
                    {                       
                        return PartialView("Edit", carDetail);
                    }
                    else
                    {
                        return PartialView("Error");
                    }
                }
                else
                {
                    return PartialView("Error");
                }
            }
            
        }
        
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult DeleteCarData(int CarId)
        {
            using (var client = new HttpClient())
            {
                CarDetail carDetail = new CarDetail();
                client.BaseAddress = new Uri("https://localhost:7161/api/");
                //HTTP GET
                var responseTask = client.DeleteAsync("CarDetails/" + CarId);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View("Error");
        }

        public IActionResult Delete(int CarId)
        {
            using (var client = new HttpClient())
            {
                CarDetail carDetail = new CarDetail();
                client.BaseAddress = new Uri("https://localhost:7161/api/");
                //HTTP GET
                var responseTask = client.GetAsync("CarDetails/" + CarId);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<CarDetail>();
                    readTask.Wait();
                    carDetail = readTask.Result;

                    if (carDetail != null)
                    {
                        return PartialView("Delete", carDetail);
                    }
                    else
                    {
                        return PartialView("Error");
                    }
                }
                else
                {
                    return PartialView("Error");
                }
            }
        }
        public IActionResult SaveCreate(CarDetail car)
            {
            HttpClientHandler handler = new HttpClientHandler();
            if (car!= null)
            {
                using (var client = new HttpClient(handler,false))
                {

                    CarDetail carDetail = new CarDetail();
                    client.BaseAddress = new Uri("https://localhost:7161/api/");
                    
                    var carUpdate = JsonSerializer.Serialize<CarDetail>(car);
                    var responseTask = client.PostAsync("CarDetails", new StringContent(carUpdate, Encoding.UTF8, "application/json"));
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return View();
        }

        public IActionResult UpdateCarDetails(CarDetail car)
        {
            HttpClientHandler handler = new HttpClientHandler();
            IEnumerable<CarDetail> carDetails = null;
            if (car != null)
            {
                using (var client = new HttpClient(handler,false))
                {
                    
                    CarDetail carDetail = new CarDetail();
                    client.BaseAddress = new Uri("https://localhost:7161/api/");
                    //HTTP GET
                    var carUpdate = JsonSerializer.Serialize<CarDetail>(car);
                    var responseTask = client.PutAsync("CarDetails/"+car.CarId , new StringContent(carUpdate, Encoding.UTF8, "application/json"));
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return PartialView("Error");
        }
    }
}
