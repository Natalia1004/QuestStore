using System.Linq;
using Microsoft.AspNetCore.Mvc;
using QuestStoreNAT.web.Models;
using QuestStoreNAT.web.DatabaseLayer;
using QuestStoreNAT.web.Services;


namespace QuestStoreNAT.web.Controllers
{
    public class MessageController : Controller
    {
        private StudentDAO studentDAO;
        private StudentAcceptanceDAO studentAcceptanceDAO;
        private GroupTransactionDAO groupTransactionDAO;
        private readonly ICurrentSession _session;

        public MessageController(ICurrentSession session)
        {
            studentDAO = new StudentDAO();
            groupTransactionDAO = new GroupTransactionDAO();
            studentAcceptanceDAO = new StudentAcceptanceDAO();
            _session = session;
        }

        [HttpGet]
        public IActionResult Message()
        {
            var currentUser = _session.LoggedUser;
            var currentStudent = studentDAO.FindOneRecordBy(currentUser.CredentialID);
            var model = new StudentAcceptanceDAO().FindOneRecordBy(currentStudent.Id);
            if (model != null)
            {
                model.costArtifact = new ArtifactDAO().FindOneRecordBy(model.artifactID).Cost;
                model.artifactName = new ArtifactDAO().FindOneRecordBy(model.artifactID).Name;
                model.currentAmountOfAcceptance = groupTransactionDAO.FindOneRecordBy(model.groupID).numberOfAcceptance;
                return View(model);
            }
            TempData["Message"] = $"You don't have any message";
            return RedirectToAction("Welcome", "Profile");
        }

        [HttpPost]
        public IActionResult Acceptance(StudentAcceptance studentAcceptance)
        {
            var currentUser = _session.LoggedUser;
            var currentStudent = studentDAO.FindOneRecordBy(currentUser.CredentialID);
            var currentStudentAcceptanceToUpdate = studentAcceptanceDAO.FindOneRecordBy(currentStudent.Id);
            var studentGroup = new GroupDAO().FindOneRecordBy(currentStudent.GroupID);
            studentGroup.GroupStudents = new StudentDAO().FetchAllStudentInGroup(currentStudent.GroupID);
            var artifactToBuy = new ArtifactDAO().FindOneRecordBy(currentStudentAcceptanceToUpdate.artifactID);
            int amountStudents = studentGroup.GroupStudents.Count();
            var model = new OwnedArtifactGroupDAO();
            var newRecord = new OwnedArtifactGroup()
            {
                GroupId = currentStudent.GroupID,
                ArtifactId = currentStudentAcceptanceToUpdate.artifactID,
                CompletionStatus = 0,
            };
            if (ModelState.IsValid)
            {
                if(studentAcceptance.acceptance == 2)
                {
                    studentAcceptanceDAO.DeleteRecord(currentStudentAcceptanceToUpdate.ID);
                    var currentGroupTransaction = groupTransactionDAO.FindOneRecordBy(currentStudentAcceptanceToUpdate.groupID);
                    var currentNumberOfAcceptance = currentGroupTransaction.numberOfAcceptance + 1;
                    currentGroupTransaction.numberOfAcceptance = currentNumberOfAcceptance;
                    groupTransactionDAO.UpdateRecord(currentGroupTransaction);
                    if (currentGroupTransaction.numberOfAcceptance == currentGroupTransaction.numberOfStudents)
                    {
                        foreach (Student student in studentGroup.GroupStudents)
                        {
                            int currentWalletValue = student.Wallet - (artifactToBuy.Cost / amountStudents);
                            student.Wallet = currentWalletValue;
                            new StudentDAO().UpdateRecord(student);
                        }
                        groupTransactionDAO.DeleteAllTransactionForGroup(currentStudentAcceptanceToUpdate.groupID);
                        model.AddRecord(newRecord);
                        return RedirectToAction("ShowStudentProfile", "Profile");
                    }
                    return RedirectToAction("ShowStudentProfile", "Profile");
                }
                else if(studentAcceptance.acceptance == 1)
                {
                    studentAcceptanceDAO.DeleteAllTransactionForGroup(currentStudentAcceptanceToUpdate.groupID);
                    groupTransactionDAO.DeleteAllTransactionForGroup(currentStudentAcceptanceToUpdate.groupID);
                    return RedirectToAction("ShowStudentProfile", "Profile");
                }
                
            }
            return RedirectToAction("Error", "Home");
        }
    }
}
