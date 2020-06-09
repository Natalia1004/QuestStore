using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuestStoreNAT.web.Models;

namespace QuestStoreNAT.web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly Student testStudentModel;

        public ProfileController()
        {
            testStudentModel = new Student();
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
