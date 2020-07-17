using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuestStoreNAT.web.DatabaseLayer;
using QuestStoreNAT.web.Models;
using QuestStoreNAT.web.Services;
using QuestStoreNAT.web.ViewModels;

namespace QuestStoreNAT.web.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentDAO _studentDAO;
        private readonly QuestDAO _questDAO;
        private readonly OwnedQuestStudentDAO _ownedQuestStudentDAO;
        private readonly ICurrentSession _session;
        private static int studentId;

        public StudentController(StudentDAO studentDAO, 
                                QuestDAO questDAO,
                                OwnedQuestStudentDAO ownedQuestStudentDAO,
                                ICurrentSession session)
        {
            _studentDAO = studentDAO;
            _questDAO = questDAO;
            _ownedQuestStudentDAO = ownedQuestStudentDAO;
            _session = session;
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

        public IActionResult Details(int Id)
        {
            return View(GetStudentDetails(Id));

        }

        public IActionResult Confirmation(int questId)
        {
            var questToConfirm = _ownedQuestStudentDAO.FetchAllRecords().FirstOrDefault(q => q.Id == questId);
            questToConfirm.CompletionStatus = CompletionStatus.Finished;
            _ownedQuestStudentDAO.UpdateRecord(questToConfirm);
            return RedirectToAction("Details", "Student", new { Id = studentId });
        }

        #region priv
        private Student GetStudentDetails(int Id)
        {
            studentId = Id;
            var student = _studentDAO.FetchAllRecords().FirstOrDefault(s => s.Id == Id);
            var studentQuests = _ownedQuestStudentDAO.FetchAllRecords().Where(q => q.StudentId == student.Id).ToList();
            var quests = _questDAO.FetchAllRecords();

            student.OwnedStudentQuests = quests
                                         .Join(studentQuests, q => q.Id, s => s.QuestId, (q, s) => new OwnedQuestStudent
                                               {Id = s.Id, StudentId = s.StudentId, QuestId = s.QuestId, CompletionStatus = s.CompletionStatus,Name = q.Name,Cost = q.Cost})
                                          .OrderBy(q=>q.CompletionStatus)
                                          .ToList();
            return student;
        }
        #endregion
    }
}
