using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class TopTenItem
    {
        public string Name { get; set; }
        public int NumLikes { get; set; }
        public string Author { get; set; }
        public ObjectId Id { get; set; }
    }
}