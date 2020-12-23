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
        private readonly ISession _session;
        public LoginController(ILogger<LoginController> logger,
           IUserManager usermanager,
           ISession session) {
            _logger = logger;
            _usermanager = usermanager;
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
                var message = System.Net.WebUtility.UrlEncode( "The user has not been found.");
                return RedirectToPage("Home/Index?error=" + message);
            }
        }

        public IActionResult AnonymousLogin() {
            var anonymousUser = _usermanager.CreateAnonymousLogin();
            _session.SetObject("User", anonymousUser);
            return RedirectToPage("Blog/Index");

        }
    }
}
