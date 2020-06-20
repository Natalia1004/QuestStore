using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using QuestStoreNAT.web.DatabaseLayer;
using QuestStoreNAT.web.Models;

namespace QuestStoreNAT.web.Controllers
{
    public class ClassroomController : Controller
    {
        private readonly ClassroomDAO _classroomDAO;

        public ClassroomController(ClassroomDAO classroomDAO)
        {
            _classroomDAO = classroomDAO;
        }

        public IActionResult Index()
        {
            return View(_classroomDAO.FetchAllRecords().OrderBy(c =>c.Id));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] Classroom classroom )
        {
            _classroomDAO.AddRecord(classroom);
            return RedirectToAction("Edit" , "Classroom");
        }

        public IActionResult Edit(int id)
        {
            var classroom = _classroomDAO.FindOneRecordBy(id);
            return View(classroom);
        }

        [HttpPost]
        public IActionResult Edit( Classroom classroom )
        {
            _classroomDAO.UpdateRecord(classroom);
            return RedirectToAction("Index", "Classroom");
        }
        public IActionResult Delete( int id )
        {
            _classroomDAO.DeleteRecord(id);
            return RedirectToAction("Index" , "Classroom");
        }
    }
}
