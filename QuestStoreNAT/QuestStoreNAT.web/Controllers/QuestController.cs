using Microsoft.AspNetCore.Mvc;
using QuestStoreNAT.web.DatabaseLayer;
using QuestStoreNAT.web.Models;

namespace QuestStoreNAT.web.Controllers
{
    public class QuestController : Controller
    {
        private readonly QuestDAO questDAO;

        public QuestController()
        {
            questDAO = new QuestDAO();
        }

        [HttpGet]
        public IActionResult ViewAllQuests()
        {
            var model = questDAO.FetchAllRecords();
            return View(model);
        }

        [HttpGet]
        public IActionResult AddQuest()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddQuest(Quest questToAdd)
        {
            if (ModelState.IsValid)
            {
                questDAO.AddRecord(questToAdd);
                TempData["QuestMessage"] = $"You have succesfully added the \"{questToAdd.Name}\" Quest!";
                return RedirectToAction("ViewAllQuests", "Quest");
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public IActionResult EditQuest(int id)
        {
            var model = questDAO.FindOneRecordBy(id);
            if (model == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditQuest(Quest questToEdit)
        {
            if (ModelState.IsValid)
            {
                questDAO.UpdateRecord(questToEdit);
                TempData["QuestMessage"] = $"You have updated the \"{questToEdit.Name}\" Quest!";
                return RedirectToAction("ViewAllQuests", "Quest");
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpGet]
        public IActionResult DeleteQuest(int id)
        {
            var model = questDAO.FindOneRecordBy(id);
            if (model == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteQuest(Quest questToDelete)
        {
            questDAO.DeleteRecord(questToDelete.Id);
            TempData["QuestMessage"] = $"You have deleted the \"{questToDelete.Name}\" Quest!";
            return RedirectToAction("ViewAllQuests", "Quest");
        }
    }
}
