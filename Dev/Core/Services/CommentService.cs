using Core.Domain;
using Core.Helpers;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class CommentService
    {
        private readonly MongoHelper<Comment> mhComment;

        public CommentService()
        {
            mhComment = new MongoHelper<Comment>();
        }

        public IList<Comment> GetComments()
        {
            var comments = mhComment.Collection;
            MongoDB.Driver.Builders.SortByBuilder sort = new MongoDB.Driver.Builders.SortByBuilder();
            sort.Descending("Date");
            return comments.FindAll().SetSortOrder(sort).ToList<Comment>();
        }

        public void AddComment(Comment comment)
        {
            mhComment.Collection.Insert(comment);
        }

        public Comment GetOne(ObjectId id)
        {
            return mhComment.Collection.FindOneById(id);
        }

    }
}
