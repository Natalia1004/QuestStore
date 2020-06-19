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

        public IActionResult Edit(int studentId)
        {
            var studentToEdit = _studentDAO.FindOneRecordBy(studentId);
            return View(studentToEdit);
        }

        [HttpPost]
        public IActionResult Edit(Student editedStudent)
        {
            var allStudents = _studentDAO.FetchAllRecords();
            var studentToEdit = allStudents.FirstOrDefault(m => m.Id == editedStudent.Id);
            _studentDAO.UpdateRecord(studentToEdit);
            return RedirectToAction("Index" , "Student");
        }
    }
}
