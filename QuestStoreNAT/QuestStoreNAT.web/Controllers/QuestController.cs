using Microsoft.AspNetCore.Mvc;
using QuestStoreNAT.web.DatabaseLayer;
using QuestStoreNAT.web.Models;

namespace QuestStoreNAT.web.Controllers
{
    public class QuestController : Controller
    {
        private readonly IDB_GenericInterface<Quest> _questDAO;

        public QuestController(IDB_GenericInterface<Quest> questDAO)
        {
            _questDAO = questDAO;
        }

        [HttpGet]
        public IActionResult ViewAllQuests()
        {
            var model = _questDAO.FetchAllRecords();
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
                _questDAO.AddRecord(questToAdd);
                TempData["QuestMessage"] = $"You have succesfully added the \"{questToAdd.Name}\" Quest!";
                return RedirectToAction($"ViewAllQuests", $"Quest");
            }
            Response.StatusCode = 406;
            ViewBag.ErrorMessage = "Sorry, adding new Quest failed.";
            return View($"Error");
        }

        [HttpGet]
        public IActionResult EditQuest(int id)
        {
            var model = _questDAO.FindOneRecordBy(id);
            if (model == null)
            {
                Response.StatusCode = 404;
                ViewBag.ErrorMessage = "Sorry, you cannot edit this Quest!";
                return View($"NotFound", id);
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditQuest(Quest questToEdit)
        {
            if (ModelState.IsValid)
            {
                _questDAO.UpdateRecord(questToEdit);
                TempData["QuestMessage"] = $"You have updated the \"{questToEdit.Name}\" Quest!";
                return RedirectToAction($"ViewAllQuests", $"Quest");
            }
            Response.StatusCode = 406;
            ViewBag.ErrorMessage = "Sorry, editing this Quest failed.";
            return View($"Error");
        }

        [HttpGet]
        public IActionResult DeleteQuest(int id)
        {
            var model = _questDAO.FindOneRecordBy(id);
            if (model == null)
            {
                Response.StatusCode = 404;
                ViewBag.ErrorMessage = "Sorry, you cannot delete this Quest!";
                return View($"NotFound", id);
            }
            return View($"DeleteQuest", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteQuest(Quest questToDelete)
        {
            var questToDeleteFromDB = _questDAO.FindOneRecordBy(questToDelete.Id);
            _questDAO.DeleteRecord(questToDelete.Id);
            TempData["QuestMessage"] = $"You have deleted the \"{questToDeleteFromDB.Name}\" Quest!";
            return RedirectToAction($"ViewAllQuests", $"Quest");
        }
    }
}
