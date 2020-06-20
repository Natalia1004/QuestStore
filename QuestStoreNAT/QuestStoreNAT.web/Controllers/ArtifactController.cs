using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuestStoreNAT.web.DatabaseLayer;
using QuestStoreNAT.web.Models;
using Microsoft.AspNetCore.Http;
using QuestStoreNAT.web.Services;
using System.Runtime.ConstrainedExecution;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuestStoreNAT.web.Controllers
{
    public class ArtifactController : Controller
    {
        private readonly ArtifactDAO artifactDAO;
        private readonly ICurrentSession _session;

        public ArtifactController(ICurrentSession session)
        {
            artifactDAO = new ArtifactDAO();
            _session = session;
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
                TempData["Message"] = $"You have succesfully added the \"{artifactToAdd.Name}\" Artifact!";
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
                TempData["Message"] = $"You have updated the \"{artifactToEdit.Name}\" Artifact!";
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
            TempData["Message"] = $"You have deleted the \"{artifactToDelete.Name}\" Artifact!";
            return RedirectToAction("ViewAllArtifacts", "Artifact");
        }
        
        public IActionResult BuyArtifact(int id)
        {
            var currentUser = _session.LoggedUser;
            var currentStudent = new StudentDAO().FindOneRecordBy(currentUser.CredentialID);
            var artifactToBuy = new ArtifactDAO().FindOneRecordBy(id);
            var model = new OwnedArtifactStudentDAO();
            var newRecord = new OwnedArtifactStudent()
            {
                StudentId = currentStudent.Id,
                ArtifactId = id,
                CompletionStatus = 0,
            };
            if (currentStudent.Wallet < artifactToBuy.Cost)
            {
                TempData["Message"] = $"You don't have enough money. Sorry!";
                return RedirectToAction("ViewAllArtifacts", "Artifact");
            }
            int currentWalletValue = currentStudent.Wallet - artifactToBuy.Cost;
            currentStudent.Wallet = currentWalletValue;
            new StudentDAO().UpdateRecord(currentStudent);
            model.AddRecord(newRecord);
            TempData["Message"] = $"You bought Artifact!";
            return RedirectToAction("ViewAllArtifacts", "Artifact");
        }
        

    }
}

