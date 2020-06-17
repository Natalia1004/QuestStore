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
            return View();
        }

        public IActionResult Create(int id)
        {
            var mentorId = _mentorDAO.AddMentorByCredentialsReturningID(id);
            return RedirectToAction("Edit" , "Mentor" , mentorId);
        }

        public IActionResult Edit(int mentorId)
        {
            var mentorToEdit = _mentorDAO.FindOneRecordBy(mentorId);
            return View(mentorToEdit);
        }

        public IActionResult Edit(Mentor editedMentor)
        {
            var allMentors = _mentorDAO.FetchAllRecords();
            var mentorToEdit = allMentors.FirstOrDefault(m => m.Id == editedMentor.Id);
            _mentorDAO.UpdateRecord(mentorToEdit);
            return RedirectToAction("Index", "Admin");
        }
    }
}
