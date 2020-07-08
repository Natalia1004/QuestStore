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
        private StudentDAO studentDAO;
        private readonly ICurrentSession _session;

        public MessageController(ICurrentSession session)
        {
            studentDAO = new StudentDAO();
            _session = session;
        }

        [HttpGet]
        public IActionResult Message()
        {
            var currentUser = _session.LoggedUser;
            var currentStudent = new StudentDAO().FindOneRecordBy(currentUser.CredentialID);
            var model = new StudentAcceptanceDAO().FindOneRecordBy(currentStudent.Id);
            return View(model);
        }

        /*[HttpPost]
        public IActionResult Acceptance(StudentAcceptance acceptance)
        {

        }*/
    }
}
