using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class Comment
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Email { get; set; }

        public DateTime Date { get; set; }

        public string comment { get; set; }
    }
}
