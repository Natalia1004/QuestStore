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

        public IActionResult Details(int id)
        {
            return View(GetStudentDetails(id));

        }

        public IActionResult Confirmation(int questId)
        {
            //var checkid = model.QuestIDToUpdate;
            var questToConfirm = _ownedQuestStudentDAO.FetchAllRecords().FirstOrDefault(q => q.Id == questId);
            questToConfirm.CompletionStatus = 0;
            _ownedQuestStudentDAO.UpdateRecord(questToConfirm);
            var Id = _session.LoggedUser.CredentialID;
            return RedirectToAction("Details", "Student", new { id = Id });// nie przekazuje Id do Details
        }

        #region priv
        private Student GetStudentDetails(int id)
        {
            var student = _studentDAO.FetchAllRecords().FirstOrDefault(s => s.CredentialID == id);
            var studentQuests = _ownedQuestStudentDAO.FetchAllRecords().Where(q => q.StudentId == student.Id).ToList();
            var quests = _questDAO.FetchAllRecords();

            student.OwnedStudentQuests = quests.Join(studentQuests, q => q.Id, s => s.QuestId, (q, s) => new OwnedQuestStudent
                                               {Id = s.Id, StudentId = s.StudentId, QuestId = s.QuestId, 
                                                CompletionStatus = s.CompletionStatus,Name = q.Name,Cost = q.Cost})
                                                .OrderBy(q=>q.CompletionStatus)
                                                .ToList();
            

            return student;
        }
        #endregion
    }
}
