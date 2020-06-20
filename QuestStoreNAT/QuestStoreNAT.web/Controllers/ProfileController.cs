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
    public class ProfileController : Controller
    {
        private readonly ICurrentSession _session;

        public ProfileController(ICurrentSession session)
        {
            _session = session;
        }

        public IActionResult Welcome()
        {
            ViewData["role"] = _session.LoggedUserRole;
            var model = _session.LoggedUser;
            return View(model);
        }

        public IActionResult MyProfile()
        {
            ViewData["role"] = _session.LoggedUserRole;
            return View();
        }

        public IActionResult ShowStudentProfile()
        {
            ViewData["role"] = _session.LoggedUserRole;
            var model = _session.LoggedUser;
            var CredentialID = model.CredentialId;
            var Student = new StudentDAO();
            var targetStudent = Student.FindOneRecordBy(CredentialID);
            targetStudent.level = new LevelStudent().levelStudent(targetStudent.OverallWalletLevel);
            targetStudent.StudentArtifacts = new ArtifactDAO().FetchAllRecords(targetStudent.Id, 0);
            targetStudent.UsedStudentArtifacts = new ArtifactDAO().FetchAllRecords(targetStudent.Id, 1);
            return View(targetStudent);
        }

        public IActionResult UseArtifact(int id)
        {
            ViewData["role"] = _session.LoggedUserRole;
            var student = _session.LoggedUser;
            var currentStudent = new StudentDAO().FindOneRecordBy(student.CredentialId);
            var artifactToBuy = new ArtifactDAO().FindOneRecordBy(id);
            var ownedArtifactStudentDAO = new OwnedArtifactStudentDAO();
            var model = ownedArtifactStudentDAO.FindOneRecordBy(id, currentStudent.Id);
            ownedArtifactStudentDAO.DeleteRecord(model.Id);
            return RedirectToAction("ShowStudentProfile", "Profile");
        }
    }
}
