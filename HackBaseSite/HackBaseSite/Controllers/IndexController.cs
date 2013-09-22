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
        public const string ConnectionString = "mongodb://54.225.128.9:27017";
        public const string DatabaseName = "HackDb";
        
        //
        // GET: /Index/
        public ActionResult Index()
        {
            var database = new MongoClient(IndexController.ConnectionString).GetServer().GetDatabase(IndexController.DatabaseName);
            var collection = database.GetCollection<Models.HackIdea_Id>("HackIdeas");

            var top10 = collection.FindAll().OrderByDescending(h => h.NumLikes).Take(10)
                .Select(h => new Models.TopTenItem {
                    Author = h.Author,
                    Id = h.Id,
                    Name = h.Name,
                    NumLikes = h.NumLikes
                })
            .ToList();

            return View(top10);
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
        public ActionResult Like(string id, string userId)
        {
            var query = Query<Models.HackIdea_Id>.EQ(f => f.Id, new ObjectId(id));
            var database = new MongoClient(IndexController.ConnectionString).GetServer().GetDatabase(IndexController.DatabaseName);
            var collection = database.GetCollection<Models.HackIdea_Id>("HackIdeas");
            var collection2 = database.GetCollection<Models.Author>("Users");

            var hack = collection.FindOne(query);

            //If this hasn't been liked before
            
            var person = collection2.FindOneById(userId);
            if (person == null || !person.Likes.Contains(id))
            {
                ++hack.NumLikes;
                collection.Save(hack);

                if (person == null)
                {
                    person = new Models.Author { Id = userId, Likes = "" };
                }
                person.Likes = person.Likes + "," + id;
                collection2.Save(person);   
            }

            var json = new JsonResult();
            if (hack != null)
                json.Data = hack.NumLikes;
            else
                json.Data = -1;

            return json;
        }

    }
}
