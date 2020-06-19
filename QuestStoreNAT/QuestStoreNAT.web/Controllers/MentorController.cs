using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuestStoreNAT.web.DatabaseLayer;
using QuestStoreNAT.web.Models;

namespace QuestStoreNAT.web.Controllers
{
    public class MentorController : Controller
    {
        private readonly MentorDAO _mentorDAO;

        public MentorController(MentorDAO mentorDAO)
        {
            _mentorDAO = mentorDAO;
        }
        public IActionResult Index()
        {
            return View(_mentorDAO.FetchAllRecords());
        }

        public IActionResult Create(int credentialId)
        {
            var mentorId = _mentorDAO.AddMentorByCredentialsReturningID(credentialId);
            return RedirectToAction("Edit" , "Mentor" , mentorId);
        }

        public IActionResult Edit(int mentorId)
        {
            var mentorToEdit = _mentorDAO.FindOneRecordBy(mentorId);
            return View(mentorToEdit);
        }

        [HttpPost]
        public IActionResult Edit(Mentor editedMentor)
        {
            _mentorDAO.UpdateRecord(editedMentor);
            return RedirectToAction("Index", "Mentor");
        }

        public IActionResult Delete(int id)
        {
            _mentorDAO.DeleteRecord(id);
            return RedirectToAction("Index" , "Mentor");
        }

        public IActionResult Details( int id )
        {
            var mentor = _mentorDAO.FetchAllRecords()
                .FirstOrDefault(m => m.Id == id);
            return View(mentor);
        }
    }
}
