using BloggingApp.Interfaces;
using BloggingApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BloggingApp.Helpers;
using BloggingApp.ViewModel;

namespace BloggingApp.Controllers {
    public class LoginController : Controller {
        private readonly ILogger<LoginController> _logger;
        private readonly IUserManager _usermanager;
        private  ISession _session;
        public LoginController(ILogger<LoginController> logger,
           IUserManager usermanager
           ) {
            _logger = logger;
            _usermanager = usermanager;
            if(HttpContext != null)
                _session = HttpContext.Session;
        }


        public void SetISession(ISession session) {
            _session = session;
        }
  
        public ActionResult Index() {
           
            return View();
        }

        public IActionResult Login([FromBody] ModelWiewUser User) {
            var userfound = _usermanager.ValidateUser(User.Login, User.Password);
            if(userfound != null) {
                _session.SetObject("User", userfound);
                return RedirectToPage("Blog/Index");
            }
            else {
                //this message should be in a languaje dictinary file
                var message = "The user has not been found.";
                return RedirectToPage("Home/Index", new { error = message });
            }
        }

        public IActionResult AnonymousLogin() {
            var anonymousUser = _usermanager.CreateAnonymousLogin();
            _session.SetObject("User", anonymousUser);
            return RedirectToPage("Blog/Index");

        }
    }
}
