using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace QuestStoreNAT.web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found!";
                    ViewBag.Path = statusCodeResult.OriginalPath;
                    ViewBag.QueryString = statusCodeResult.OriginalQueryString;
                    return View("NotFound");
                case 500:
                    ViewBag.ErrorMessage = "Sorry, there was an server error. Try later or...";
                    ViewBag.Path = statusCodeResult.OriginalPath;
                    ViewBag.QueryString = statusCodeResult.OriginalQueryString;
                    return View("Error");
            }
            return View("Error");
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            ViewBag.ExceptionPath = exceptionDetails.Path;
            ViewBag.ExceptionMessage = exceptionDetails.Error.Message;
            ViewBag.StackTrace = exceptionDetails.Error.StackTrace;

            return View("Error");
        }
    }
}
