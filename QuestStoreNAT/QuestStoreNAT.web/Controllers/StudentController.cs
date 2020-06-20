using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuestStoreNAT.web.DatabaseLayer;
using QuestStoreNAT.web.Models;

namespace QuestStoreNAT.web.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentDAO _studentDAO;

        public StudentController(StudentDAO studentDAO)
        {
            _studentDAO = studentDAO;
        }
        public IActionResult Index()
        {
            return View(_studentDAO.FetchAllRecords());
        }

        public IActionResult Create(int id)
        {
            var studentId = _studentDAO.AddStudentByCredentialsReturningID(id);
            return RedirectToAction("Edit" , "Student" , studentId);
        }

        public IActionResult Edit(int id)
        {
            var studentToEdit = _studentDAO.FindOneRecordBy(id);
            return View(studentToEdit);
        }

        [HttpPost]
        public IActionResult Edit(Student Student)
        {
            _studentDAO.UpdateRecord(Student);
            return RedirectToAction("Index" , "Student");
        }

        public IActionResult Delete( int id )
        {
            _studentDAO.DeleteRecord(id);
            return RedirectToAction("Index" , "Mentor");
        }
    }
}
