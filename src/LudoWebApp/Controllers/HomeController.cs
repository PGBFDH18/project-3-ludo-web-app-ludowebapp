using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LudoWebApp.Models;
using WebAPI.Controllers;
using WebAPI.Models;
using LudoGameEngine;

namespace LudoWebApp.Controllers
{
    public class HomeController : Controller
    {

    //    public interface ILudoModels : ILudoGame
    //    {

    //        [HttpPost("/ludo/createnewgame")]
    //        ITask<Person> AddPersonAsync([JsonContent]Person person);

            
    //    }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Home Controllers";

            return View();
        }
        

        //[HttpPost("newludogame")]
        public IActionResult Ludo()
        {
            //var config = new 
            //{
            //    HttpHost = new Uri("https://localhost:44370/newludogame"),
            //};

            ////var client = HttpPostAttribute;
            ////ViewData["Message"] = "Creating new Ludo Game.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

