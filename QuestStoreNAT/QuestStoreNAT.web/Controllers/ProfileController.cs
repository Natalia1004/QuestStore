using Microsoft.AspNetCore.Mvc;
using QuestStoreNAT.web.Models;
using QuestStoreNAT.web.DatabaseLayer;
using QuestStoreNAT.web.Services;

namespace QuestStoreNAT.web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ICurrentSession _session;
        private int _credentialID { get; set; }
        private StudentDAO _studentDAO { get; set; }
        private int _studentID { get; set; }

        public ProfileController(ICurrentSession session)
        {
            _session = session;
            _credentialID = _session.LoggedUser.CredentialID;
            _studentDAO = new StudentDAO();
            _studentID = _studentDAO.FindOneRecordBy(_credentialID).Id;
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
            var targetStudent = new StudentDetails().ShowStudentDetails(_studentID);
            return View(targetStudent);
        }

        public IActionResult UseArtifact(int id)
        {
            ViewData["role"] = _session.LoggedUserRole;
            new ArtifactManagement().UseArtifact(_studentID, id);
            return RedirectToAction("ShowStudentProfile", "Profile");
        }

        public IActionResult DeleteArtifact(int id)
        {
            ViewData["role"] = _session.LoggedUserRole;
            new ArtifactManagement().DeleteUsedArtifactFromView(_studentID, id);
            return RedirectToAction("ShowStudentProfile", "Profile");
        }


    }
}
