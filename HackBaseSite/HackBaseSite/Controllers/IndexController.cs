using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace HackBaseSite.Controllers
{
    public class IndexController : Controller
    {
        public const string ConnectionString = "mongodb://localhost";
        public const string DatabaseName = "HackDb";
        
        //
        // GET: /Index/
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult PostHack()
        {
            return View();
        }

        public ActionResult FillOutForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Like(string id)
        {
            var query = Query<Models.HackIdea_Id>.EQ(f => f.Id, new ObjectId(id));
            var database = new MongoClient(IndexController.ConnectionString).GetServer().GetDatabase(IndexController.DatabaseName);
            var collection = database.GetCollection<Models.HackIdea_Id>("HackIdeas");
            var hack = collection.FindOne(query);
            hack.NumLikes++;
            collection.Save(hack);
            
            return new EmptyResult();
        }

    }
}
