using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace TwitterMongo.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Core.Domain.User model)
        {
            if (ModelState.IsValid)
            {
                new Core.Services.UserService().AddUser(model);
                FormsAuthentication.SetAuthCookie(model.Email, true);
                return RedirectToAction("Index", "Comments");
              
            }
           
            return View(model);
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login 
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Core.Domain.User model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (!new Core.Services.UserService().IsUser(model))
                {
                    ModelState.AddModelError("auth", "Authentication faild");
                    return View(model);
                }
                FormsAuthentication.SetAuthCookie(model.Email, true);
                return RedirectToAction("Index", "Comments");
            }
            return View(model);
        }

        // POST: /Account/LogOff 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}