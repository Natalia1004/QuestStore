using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuestStoreNAT.web.DatabaseLayer;
using QuestStoreNAT.web.Models;
using QuestStoreNAT.web.Services;

namespace QuestStoreNAT.web.Controllers
{
    public class QuestController : Controller
    {
        private readonly ICurrentSession _session;

        public QuestController(ICurrentSession session)
        {
            _session = session;
        }

        [HttpGet]
        public IActionResult AddQuest()
        {
            ViewData["role"] = _session.LoggedUserRole;
            return View();
        }
        [HttpPost]
        public IActionResult AddQuest(Quest questToAdd)
        {
            var questDAO = new QuestDAO();
            questDAO.AddRecord(questToAdd);

            ViewData["role"] = _session.LoggedUserRole;
            return View();
        }
    }
}
