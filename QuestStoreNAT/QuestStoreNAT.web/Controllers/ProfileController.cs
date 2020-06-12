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
        private readonly IUser testStudentModel;
        private readonly ICurrentSession _session;

        public ProfileController(IUser user, ICurrentSession session)
        {
            testStudentModel = user;
            _session = session;
            testStudentModel.FirstName = "Robert";
            testStudentModel.Credentials = new Credentials();
            testStudentModel.Credentials.Role = Role.Admin;
        }

        public IActionResult Welcome()
        {
            ViewData["role"] = _session.LoggedUserRole;
            //ten widok działa na modelu IUser
            return View(testStudentModel);
        }

        public IActionResult MyProfile()
        {
            return View();
        }

    }
}
