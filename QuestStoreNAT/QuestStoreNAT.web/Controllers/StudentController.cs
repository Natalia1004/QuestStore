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
            return View(_studentDAO.FetchAllRecords().OrderBy(s => s.Id).ToList());
        }

        public IActionResult Create(int id)
        {
            var Id = _studentDAO.AddStudentByCredentialsReturningID(id);
            return RedirectToAction("Edit" , "Student" , new { Id = Id });
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var studentToEdit = _studentDAO.FindOneRecordBy(Id);
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
            return RedirectToAction("Index" , "Student");
        }
    }
}
