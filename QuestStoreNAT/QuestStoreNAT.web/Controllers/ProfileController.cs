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
    public class ProfileController : Controller
    {
        private readonly ICurrentSession _session;

        public ProfileController(ICurrentSession session)
        {
            _session = session;
        }

        public IActionResult Welcome()
        {
            ViewData["role"] = _session.LoggedUserRole;
            var model = _session.LoggedUser;
            return View(model);
        }

        public IActionResult MyProfile()
        {
            ViewData["role"] = _session.LoggedUserRole;
            return View();
        }
    }
}
