using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using QuestStoreNAT.web.DatabaseLayer;

namespace QuestStoreNAT.web.Controllers
{
    public class ClassEnrolmentController : Controller
    {
        private readonly ClassEnrolmentDAO _classEnrolmentDAO;

        public ClassEnrolmentController(ClassEnrolmentDAO classEnrolmentDAO)
        {
            _classEnrolmentDAO = classEnrolmentDAO;
        }
        public IActionResult Index()
        {
            var allRecords = _classEnrolmentDAO.FetchAllRecords();
            if (allRecords == null)
            {
                return View("there has been no class enrolment so far");
            }
            return View(allRecords);
        }
    }
}
