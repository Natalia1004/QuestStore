using Microsoft.AspNetCore.Mvc;
using QuestStoreNAT.web.DatabaseLayer;
using QuestStoreNAT.web.Models;
using QuestStoreNAT.web.Services;

namespace QuestStoreNAT.web.Controllers
{
    public class ArtifactController : Controller
    {
        private StudentDAO studentDAO;
        private Student _student { get; set; }
        private ArtifactDAO artifactDAO;
        private readonly ICurrentSession _session;
        private int _credentialID { get; set; }
        public ArtifactManagement artifactManagmenet { get; set; }

        public ArtifactController(ICurrentSession session)
        {
            artifactDAO = new ArtifactDAO();
            studentDAO = new StudentDAO();
            _session = session;
            _credentialID = _session.LoggedUser.CredentialID;
            artifactManagmenet = new ArtifactManagement();
            _student = studentDAO.FindOneRecordBy(_credentialID);
        }

        [HttpGet]
        public IActionResult ViewAllArtifacts()
        {
            var model = artifactDAO.FetchAllRecords();
            return View(model);
        }

        [HttpGet]
        public IActionResult AddArtifact()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddArtifact(Artifact artifactToAdd)
        {
            if (ModelState.IsValid)
            {
                artifactDAO.AddRecord(artifactToAdd);
                TempData["ArtifactMessage"] = $"You have succesfully added the \"{artifactToAdd.Name}\" Artifact!";
                return RedirectToAction("ViewAllArtifacts", "Artifact");
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public IActionResult EditArtifact(int id)
        {
            var model = artifactDAO.FindOneRecordBy(id);
            if (model == null)
            {
                Response.StatusCode = 404;
                ViewBag.ErrorMessage = "Sorry, you cannot edit this Artifact!";
                return RedirectToAction("Error", "Home");
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditArtifact(Artifact artifactToEdit)
        {
            if (ModelState.IsValid)
            {
                artifactDAO.UpdateRecord(artifactToEdit);
                TempData["ArtifactMessage"] = $"You have updated the \"{artifactToEdit.Name}\" Artifact!";
                return RedirectToAction("ViewAllArtifacts", "Artifact");
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public IActionResult DeleteArtifact(int id)
        {
            var model = artifactDAO.FindOneRecordBy(id);
            if (model == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteArtifact(Artifact artifactToDelete)
        {
            var questToDeleteFromDB = artifactDAO.FindOneRecordBy(artifactToDelete.Id);
            artifactDAO.DeleteRecord(questToDeleteFromDB.Id);
            TempData["ArtifactMessage"] = $"You have deleted the \"{artifactToDelete.Name}\" Artifact!";
            return RedirectToAction("ViewAllArtifacts", "Artifact");
        }

        public IActionResult BuyArtifact(int id)
        {
            if (new ArtifactManagement().CheckigStudentWallet(_credentialID, id) == false)
            {
                TempData["ArtifactMessage"] = $"You don't have enough money. Sorry!";
            }
            else
            {
                new ArtifactManagement().BuyIndiviudalArtifact(_credentialID, id);
                TempData["ArtifactMessage"] = $"You bought Artifact!";
            }
            return RedirectToAction("ViewAllArtifacts", "Artifact");
        }

        public IActionResult BuyGroupArtifact(int id)
        {
            if (artifactManagmenet.CheckigGroupWallet(_student.GroupID, id, _credentialID) == false)
            {
                TempData["ArtifactMessage"] = $"Your group don't have enough money or maybe You don't have enough coolcoins to share costs. Sorry!";
            }
            else if(artifactManagmenet.CheckingIfTransactionForBoughtGroupArtifactExist(_student.GroupID) == false)
            {
                TempData["ArtifactMessage"] = $"Transactions for purchase the new group artifact exist. You can't make another transaction!";
            }
            else
            {
                artifactManagmenet.CreateNewGroupTransaction(id, _student.GroupID);
                artifactManagmenet.CreateRecordForAcceptance(_credentialID, _student.GroupID, id);
                TempData["ArtifactMessage"] = $"Your group will receive information!";
            }
            return RedirectToAction("ViewAllArtifacts", "Artifact");
        }
    }
}

