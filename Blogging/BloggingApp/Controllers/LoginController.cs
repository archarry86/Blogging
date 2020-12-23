﻿using BloggingApp.Interfaces;
using BloggingApp.Models;
using BloggingApp.ModelsView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BloggingApp.Helpers;

namespace BloggingApp.Controllers {
    public class LoginController : Controller {
        private readonly ILogger<LoginController> _logger;
        private readonly IUserManager _usermanager;

        public LoginController(ILogger<LoginController> logger,
           IUserManager usermanager) {
            _logger = logger;
            _usermanager = usermanager;
        }

        public IActionResult Login([FromBody] ModelWiewUser User) {
            var userfound = _usermanager.ValidateUser(User.Login, User.Password);
            if(userfound != null) {
                HttpContext.Session.SetObject("User", userfound);
                return RedirectToPage("Blogs/Index");
            }
            else {
                //this message should be in a languaje dictinary file
                var message = "The user has not been found.";
                return RedirectToPage("Home/Index?error=" + message);
            }
        }

        public IActionResult AnonymousLogin() {
            var anonymousUser = _usermanager.CreateAnonymousLogin();
            HttpContext.Session.SetObject("User", anonymousUser);
            return RedirectToPage("Blogs/Index");

        }
    }
}