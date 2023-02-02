using CarRentals.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Text.Json;

namespace CarRental_UI.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index(string str)
        {
            if(str == null)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else if(str.Equals("register"))
            {
                return View("Register");
            }
            else
            {
                return View("Login");
            }
            
        }
        public IActionResult Register(UserDetail user)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7161/api/");
                var userUpdate = JsonSerializer.Serialize<UserDetail>(user);
                var responseTask = client.PostAsync("UserDetails", new StringContent(userUpdate, Encoding.UTF8, "application/json"));

                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    this.TempData["Login"] = true;
                    this.TempData["email"] = user.Email;
                    
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                   return PartialView("Error");  
                }
            }
        }

        public IActionResult Login(UserDetail user)
        {
            if(user.Email == null || user.Password == null)
            {
                return PartialView("Error");
            }
            using (var client = new HttpClient())
            {
                UserDetail userDetail = new UserDetail();
                client.BaseAddress = new Uri("https://localhost:7161/api/");
                //HTTP GET
                var responseTask = client.GetAsync("UserDetails/"+user.Email);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<UserDetail>();
                    readTask.Wait();
                    userDetail = readTask.Result;

                    if(userDetail!=null && userDetail.Password.Equals(user.Password))
                    {
                        this.TempData["Login"] = true;
                        this.TempData["Admin"] = userDetail.Admin;
                        this.TempData["email"] = user.Email;
                        
                        return RedirectToAction("Index", "Dashboard");
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
    }
}
