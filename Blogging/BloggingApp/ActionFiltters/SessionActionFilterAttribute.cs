using BloggingApp.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggingApp.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace BloggingApp.ActionFiltters {
    public class SessionActionFilterAttribute : Attribute, IActionFilter {
        public void OnActionExecuted(ActionExecutedContext context) {
           
        }
        public void OnActionExecuting(ActionExecutingContext context) {
          var user = context.HttpContext.Session.GetObject<User>("User");
            if(user == null) {
                var message = "You must log on the platform either by a registered user or anonymously.";
                context.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    { "controller", "Home" },
                    { "action", "Index?"+message }
                });
            }

        }
    }
}
