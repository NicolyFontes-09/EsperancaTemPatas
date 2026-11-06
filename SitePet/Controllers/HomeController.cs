using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SitePet.Models;
using System.Diagnostics;

namespace SitePet.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
       
        public IActionResult QueroAdotar()
        {

            return View();
        }
        
        public IActionResult QuemSomos()
        {
            return View();
        }
      
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Contato()
        {
            return View();
        }


        
    }
}