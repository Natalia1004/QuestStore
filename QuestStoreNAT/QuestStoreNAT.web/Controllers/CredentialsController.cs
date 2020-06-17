using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuestStoreNAT.web.DatabaseLayer;
using QuestStoreNAT.web.Models;

namespace QuestStoreNAT.web.Controllers
{
    public class CredentialsController : Controller
    {
        private readonly CredentialsDAO _credentialsDAO;

        public CredentialsController(CredentialsDAO credentialsDAO)
        {
            _credentialsDAO = credentialsDAO;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddCredentials()
        {
            return View();
        }

        public IActionResult AddCredentials([FromForm] Credentials newCredentials)
        {
            var credentialID = _credentialsDAO.AddRecordReturningID(newCredentials);
            switch ( newCredentials.Role )
            {
                case Role.Admin:
                    return RedirectToAction("Create" , "Admin" , credentialID);
                case Role.Mentor:
                    return RedirectToAction("Create" , "Mentor" , credentialID);
                case Role.Student:
                    return RedirectToAction("Create" , "Student" , credentialID);
                default:
                    return View();
            }

        }
    }
}
