using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Test1_UserHub.Models;
using Test1_UserHub.Services;

namespace Test1_UserHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        

        public HomeController(ILogger<HomeController> logger, XMLUserService xmlUserService)
        {
            _xmlUserservice = xmlUserService;
            _logger = logger;
        }
        public XMLUserService _xmlUserservice { get; }


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ViewUsers()
        {
            return View();
        }
        public IActionResult AddUser()
        {
            return View();
        }
       
        public ActionResult SaveUser(User user)
        {
            _xmlUserservice.addUser(user);
            return RedirectToAction("ViewUsers");
        }
    }
}
