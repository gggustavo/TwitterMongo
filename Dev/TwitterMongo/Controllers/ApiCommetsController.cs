using Core.Domain;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace TwitterMongo.Controllers
{
    public class ApiCommetsController : ApiController
    {
        // GET api/GetPosts
        public dynamic GetPosts()
        {
            return new CommentService().GetComments();
        }

        // POST api/PostPost
        public HttpResponseMessage PostPost(Comment post)
        {
            post.Date = DateTime.Now;
            post.Email = HttpContext.Current.User.Identity.Name;
            Core.Services.CommentService cservice = new Core.Services.CommentService();
            cservice.AddComment(post);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, cservice.GetOne(post.Id));
            response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = post.Id }));
            return response;
        }

    }
}
