using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuestStoreNAT.web.Models;
using QuestStoreNAT.web.DatabaseLayer;

namespace QuestStoreNAT.web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUser testStudentModel;

        public ProfileController(IUser user)
        {
            testStudentModel = user;
            //testStudentModel = new Student();
            testStudentModel.FirstName = "Robert";
        }

        public IActionResult Welcome()
        {
            //ten widok działa na modelu IUser
            return View(testStudentModel);
        }

        public IActionResult MyProfile()
        {
            return View();
        }

    }
}
