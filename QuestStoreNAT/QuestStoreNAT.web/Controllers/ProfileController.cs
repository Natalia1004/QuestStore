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
            var CredentialID = model.CredentialID;
            var Student = new StudentDAO();
            var targetStudent = Student.FindOneRecordBy(CredentialID);
            targetStudent.level = new LevelStudent().levelStudent(targetStudent.OverallWalletLevel);
            targetStudent.StudentArtifacts = new ArtifactDAO().FetchAllRecords(targetStudent.Id, 0);
            targetStudent.UsedStudentArtifacts = new ArtifactDAO().FetchAllRecords(targetStudent.Id, 1);
            targetStudent.GroupArtifacts = new ArtifactDAO().FetchAllGroupArtifacts(targetStudent.GroupID, 0);
            targetStudent.UsedGroupArtifacts = new ArtifactDAO().FetchAllGroupArtifacts(targetStudent.GroupID, 1);
            return View(targetStudent);
        }

        public IActionResult UseArtifact(int id)
        {
            ViewData["role"] = _session.LoggedUserRole;
            var student = _session.LoggedUser;
            var currentStudent = new StudentDAO().FindOneRecordBy(student.CredentialID);
            var artifactToUse = new ArtifactDAO().FindOneRecordBy(id);
            int completiotStatus = 0;
            if (artifactToUse.Type == 0)
            {
                var ownedArtifactStudentDAO = new OwnedArtifactStudentDAO();
                var model = ownedArtifactStudentDAO.FindOneRecordBy(id, currentStudent.Id,completiotStatus);
                model.CompletionStatus = 1;
                ownedArtifactStudentDAO.UpdateRecord(model);
            }
            else
            {
                var ownedArtifactGroupDAO = new OwnedArtifactGroupDAO();
                var modelGroup = ownedArtifactGroupDAO.FindOneRecordBy(id, currentStudent.GroupID,completiotStatus);
                modelGroup.CompletionStatus = 1;
                ownedArtifactGroupDAO.UpdateRecord(modelGroup);
            }
            return RedirectToAction("ShowStudentProfile", "Profile");
        }

        public IActionResult DeleteArtifact(int id)
        {
            ViewData["role"] = _session.LoggedUserRole;
            var student = _session.LoggedUser;
            var currentStudent = new StudentDAO().FindOneRecordBy(student.CredentialID);
            var artifactToDelte = new ArtifactDAO().FindOneRecordBy(id);
            int completiotStatus = 1;
            if (artifactToDelte.Type == 0)
            {
                var ownedArtifactStudentDAO = new OwnedArtifactStudentDAO();
                var model = ownedArtifactStudentDAO.FindOneRecordBy(id, currentStudent.Id, completiotStatus);
                ownedArtifactStudentDAO.DeleteRecord(model.Id);
            }
            else
            {
                var ownedArtifactGroupDAO = new OwnedArtifactGroupDAO();
                var modelGroup = ownedArtifactGroupDAO.FindOneRecordBy(id, currentStudent.GroupID, completiotStatus);
                ownedArtifactGroupDAO.DeleteRecord(modelGroup.Id);
            }
            return RedirectToAction("ShowStudentProfile", "Profile");
        }


    }
}
