using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuestStoreNAT.web.Models;
using QuestStoreNAT.web.Services;

namespace QuestStoreNAT.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILoginValidatorService _loginValidatorService;

        public HomeController(ILogger<HomeController> logger, ILoginValidatorService loginValidatorService)
        {
            _logger = logger;
            _loginValidatorService = loginValidatorService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Credentials credentials)
        {
            if (_loginValidatorService.IsValidLogin(credentials))
            {
                return RedirectToAction("Contact");
            }
            return View("TestView", credentials);
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ContactForm contactForm)
        {
            if (ModelState.IsValid)
            {
                //SEND us an Email or Store the message in the database
                TempData["Message"] = "You have sent us a message. Give us a minute and we will get back to you shortly.";
                return RedirectToAction("Index");
            }
            return View("Error");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
