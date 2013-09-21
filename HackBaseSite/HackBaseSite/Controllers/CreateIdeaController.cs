using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Driver;
using System.Configuration;
using MongoDB.Driver.Builders;
using System.Linq.Expressions;

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
        public ActionResult Post(Models.HackIdea model)
        {
            string connectionString = "mongodb://localhost";
            string databaseName = "HackDb";

            model.CreatedOn = DateTime.Now;

            var client = new MongoClient(connectionString);
            var database = client.GetServer().GetDatabase(databaseName);
            var collection = database.GetCollection<Models.HackIdea>("HackIdeas");

            var result = collection.Insert(model);
            if (!result.Ok)
                throw new Exception("Hack Idea could not be inserted...");

            var collection2 = database.GetCollection<Models.HackIdea_Id>("HackIdeas");

            //Expression<Func<Models.HackIdea_Id, IEnumerable<string>>> lambda1 = (Models.HackIdea_Id f) => new List<string> { f.Name, f.Description, f.Tags, f.InProgress.ToString(), f.Author, f.CreatedOn.ToString(), f.NumLikes.ToString() };

            //var query = Query<Models.HackIdea_Id>.All(lambda1,
                //new string[] { model.Name, model.Description, model.Tags, model.InProgress.ToString(), model.Author, model.CreatedOn.ToString(), model.NumLikes.ToString() });

            var queryName = Query<Models.HackIdea_Id>.EQ(f => f.Name, model.Name);
            //var queryTime = Query<Models.HackIdea_Id>.EQ(f => f.CreatedOn, model.CreatedOn);
            var possibilities = collection2.Find(queryName);
            var hack = possibilities.Where(f => f.Description == model.Description
                                            && f.Author == model.Author).Single();

            return RedirectToAction("Index", "ViewHackIdea", new { id = hack.Id.ToString() }); ;
        }

    }
}
