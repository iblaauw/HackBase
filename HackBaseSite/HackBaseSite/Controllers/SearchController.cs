using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HackBaseSite.Controllers
{
    public class SearchController : Controller
    {
        [HttpPost]
        public ActionResult Search(string text)
        {
            var database = new MongoClient(IndexController.ConnectionString).GetServer().GetDatabase(IndexController.DatabaseName);
            var collection = database.GetCollection<Models.HackIdea_Id>("HackIdeas");

            text = text.ToLower();

            var searchResults = collection.FindAll().Where(
                h => (h.Name ?? "").ToLower().Contains(text) || (h.Author ?? "").ToLower().Contains(text) 
                    || (h.Description ?? "").ToLower().Contains(text)).ToList();

            return View(searchResults);
        }

        [HttpPost]
        public ActionResult SearchTags(string text)
        {
            return View();
        }

    }
}
