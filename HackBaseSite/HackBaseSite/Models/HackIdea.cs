using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class HackIdea
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public bool InProgress { get; set; }
        public string Author { get; set; }
        public DateTime CreatedOn { get; set; }
        public int NumLikes { get; set; }
        public string GithubRepos { get; set; }
    }

    public class HackIdea_Id
    {
        public MongoDB.Bson.ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public bool InProgress { get; set; }
        public string Author { get; set; }
        public DateTime CreatedOn { get; set; }
        public int NumLikes { get; set; }
        public string GithubRepos { get; set; }
    }
}