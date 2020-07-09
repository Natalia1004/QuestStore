﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuestStoreNAT.web.DatabaseLayer;
using QuestStoreNAT.web.Models;
using Microsoft.AspNetCore.Http;
using QuestStoreNAT.web.Services;
using System.Runtime.ConstrainedExecution;

namespace QuestStoreNAT.web.Controllers
{
    public class ArtifactController : Controller
    {
        private ArtifactDAO artifactDAO;
        private StudentDAO studentDAO;
        private readonly ICurrentSession _session;

        public ArtifactController(ICurrentSession session)
        {
            artifactDAO = new ArtifactDAO();
            studentDAO = new StudentDAO();
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
                TempData["ArrifactMessage"] = $"You have updated the \"{artifactToEdit.Name}\" Artifact!";
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
            var currentUser = _session.LoggedUser;
            var currentStudent = studentDAO.FindOneRecordBy(currentUser.CredentialID);
            var artifactToBuy = artifactDAO.FindOneRecordBy(id);
            var model = new OwnedArtifactStudentDAO();
            var newRecord = new OwnedArtifactStudent()
            {
                StudentId = currentStudent.Id,
                ArtifactId = id,
                CompletionStatus = 0,
            };
            if (currentStudent.Wallet < artifactToBuy.Cost)
            {
                TempData["ArtifactMessage"] = $"You don't have enough money. Sorry!";
                return RedirectToAction("ViewAllArtifacts", "Artifact");
            }
            int currentWalletValue = currentStudent.Wallet - artifactToBuy.Cost;
            currentStudent.Wallet = currentWalletValue;
            new StudentDAO().UpdateRecord(currentStudent);
            model.AddRecord(newRecord);
            TempData["ArtifactMessage"] = $"You bought Artifact!";
            return RedirectToAction("ViewAllArtifacts", "Artifact");
        }

        public IActionResult BuyGroupArtifact(int id)
        {
            var currentUser = _session.LoggedUser;
            var currentStudent = new StudentDAO().FindOneRecordBy(currentUser.CredentialID);
            var artifactToBuy = new ArtifactDAO().FindOneRecordBy(id);
            var studentGroup = new GroupDAO().FindOneRecordBy(currentStudent.GroupID);
            var groupTransaction = new GroupTransactionDAO();
            var studentAcceptance = new StudentAcceptanceDAO();
            var model = new OwnedArtifactGroupDAO();
            var newRecord = new OwnedArtifactGroup()
            {
                GroupId = currentStudent.GroupID,
                ArtifactId = id,
                CompletionStatus = 0,
            };
            if (studentGroup.GroupWallet < artifactToBuy.Cost)
            {
                TempData["ArtifactMessage"] = $"Your group don't have enough money. Sorry!";
                return RedirectToAction("ViewAllArtifacts", "Artifact");
            }
            //if transaction for this group doesn't exist them create 
            studentGroup.GroupStudents = new StudentDAO().FetchAllStudentInGroup(currentStudent.GroupID);
            int amountStudents = studentGroup.GroupStudents.Count();
            var newRecordGroupTransaction = new GroupTransaction()
            {
                artifactID = artifactToBuy.Id,
                groupID = currentStudent.GroupID,
                numberOfStudents = amountStudents,
                numberOfAcceptance = 1
            };
            groupTransaction.AddRecord(newRecordGroupTransaction);

            foreach(Student student in studentGroup.GroupStudents)
            {
                if (student.Id != currentStudent.Id)
                {
                    var newStudentAcceptance = new StudentAcceptance()
                    {
                        studentID = student.Id,
                        artifactID = artifactToBuy.Id,
                        acceptance = false
                    };
                    studentAcceptance.AddRecord(newStudentAcceptance);
                }

            }
            /*foreach (Student student in studentGroup.GroupStudents)
            {
                int currentWalletValue = student.Wallet - (artifactToBuy.Cost / amountStudents);
                student.Wallet = currentWalletValue;
                new StudentDAO().UpdateRecord(student);
            }

            model.AddRecord(newRecord);*/
            TempData["ArtifactMessage"] = $"Your group will retrive information!";


            return RedirectToAction("ViewAllArtifacts", "Artifact");
        }
    }
}

