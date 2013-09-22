using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HackBaseSite.Controllers
{
    public class ViewHackIdeaController : Controller
    {
        //
        // GET: /ViewHackIdea/id

        public ActionResult Index(string id)
        {
            MongoDB.Bson.ObjectId objectId = new MongoDB.Bson.ObjectId(id);
            var database = new MongoClient(IndexController.ConnectionString).GetServer().GetDatabase(IndexController.DatabaseName);
            var collection = database.GetCollection<Models.HackIdea_Id>("HackIdeas");

            var query = Query<Models.HackIdea_Id>.EQ(e => e.Id, objectId);
            Models.HackIdea_Id model = collection.FindOne(query);

            return View("hackPage",model);
        }

        [HttpPost]
        public ActionResult UpdateRepos(string id, string URL)
        {
            MongoDB.Bson.ObjectId objectId = new MongoDB.Bson.ObjectId(id);
            var database = new MongoClient(IndexController.ConnectionString).GetServer().GetDatabase(IndexController.DatabaseName);
            var collection = database.GetCollection<Models.HackIdea_Id>("HackIdeas");

            var query = Query<Models.HackIdea_Id>.EQ(e => e.Id, objectId);
            Models.HackIdea_Id model = collection.FindOne(query);

            model.GithubRepos += " "+URL;
            collection.Save(model);

            return View("hackPage", model);
        }

    }
}
