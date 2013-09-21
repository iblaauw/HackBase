using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HackBaseSite.Controllers
{
    public class CreateIdeaController : Controller
    {
        //
        // GET: /CreateIdea/

        public ActionResult Index()
        {
            return View("filloutform");
        }

        [HttpPost]
        public ActionResult Post()
        {
            return RedirectToAction("Index", "Index");
        }

    }
}
