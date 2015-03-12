using Core.Domain;
using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.Builders;

namespace Core.Services
{
    public class UserService
    {
        private readonly MongoHelper<User> mhUser;

        public UserService()
        {
            mhUser = new MongoHelper<User>();
        }

        public void AddUser(User user)
        {
            mhUser.Collection.Insert(user);
        }

        public Boolean IsUser(User user)
        {
            var query = Query.And(Query.EQ("Email", user.Email), Query.EQ("Password", user.Password));
            if (mhUser.Collection.Find(query).Count() > 0)
                return true;

            return false;
        }


    }
}
