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
            string connectionString = "mongodb://localhost";
            string databaseName = "HackDb";
            var database = new MongoClient(connectionString).GetServer().GetDatabase(databaseName);
            var collection = database.GetCollection<Models.HackIdea_Id>("HackIdeas");

            var query = Query<Models.HackIdea_Id>.EQ(e => e.Id, objectId);
            Models.HackIdea_Id model = collection.FindOne(query);

            return View("hackPage",model);
        }

    }
}
