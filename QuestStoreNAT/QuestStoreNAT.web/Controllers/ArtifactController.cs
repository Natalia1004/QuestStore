using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuestStoreNAT.web.DatabaseLayer;
using QuestStoreNAT.web.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuestStoreNAT.web.Controllers
{
    public class ArtifactController : Controller
    {
        // GET: /<controller>/
        public IActionResult Artifact()
        {
            var artifact = new ArtifactDAO();
            return View("Artifact", artifact.GetAllRows());
        }
    }
}
