using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuestStoreNAT.web.Models;
using QuestStoreNAT.web.DatabaseLayer;
using Microsoft.AspNetCore.Http;
using QuestStoreNAT.web.Services;
using System.Runtime.ConstrainedExecution;


namespace QuestStoreNAT.web.Controllers
{
    public class MessageController : Controller
    {

        [HttpGet]
        public IActionResult Message()
        {
            var model = new StudentAcceptance()
            return View();
        }

        [HttpPost]
        public IActionResult Acceptance(StudentAcceptance acceptance)
        {

        }
    }
}
