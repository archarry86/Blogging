using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BloggingApp.ActionFiltters;
using BloggingApp.ViewModel;
using BloggingApp.Models;
using BloggingApp.Interfaces;
using Microsoft.Extensions.Logging;
using BloggingApp.Helpers;

namespace BloggingApp.Controllers {
    public class BlogController : Controller {

        private readonly ILogger<BlogController> _logger;
        private readonly IBlogPublication _blogPublication;
        //Instead of having this attribute . The interface IBlogPublicationFetcher  could have extened form IBlogPublication
        private readonly IBlogPublicationFetcher _blogPublicationFetcher;
        private  ISession _session;
        private  User user;
        public BlogController(ILogger<BlogController> logger,
        IBlogPublication blogPublication,
        IBlogPublicationFetcher blogPublicationFetcher) {
            _logger = logger;
            _blogPublication = blogPublication;
            _blogPublicationFetcher = blogPublicationFetcher;
            if(HttpContext != null)
            _session = HttpContext.Session;
        }

        
          public void SetISession(ISession session) {
            _session = session;
        }

        private User _GetUserFromSession() {
            if(this.user == null) {
                this.user = _session.GetObject<User>("User");
            }
            return user;
        }

        /// <summary>
        /// this method is call on unit test
        /// Because I could not set the 
        /// </summary>
        /// <param name="user"></param>
        public void SetUser(User user) {
            this.user = user;
        }

        // GET: BlogController
        [SessionActionFilter]
        public ActionResult Index() {
            var user =  this._GetUserFromSession();
            var blogs = _blogPublication.GetBlogs(user);
            return View(blogs);
        }

        [SessionActionFilter]
        // GET: BlogController/Details/5
        public ActionResult Details(long id) {
            var user = this._GetUserFromSession();
            var blog  = _blogPublication.GetBlogById(id, user);
            if( blog != null 
                &&(
                user.Role.RolType == RolType.editor 
                ||
                  (user.Role.RolType != RolType.editor &&
                    ( blog.BlogStatus == BlogStatus.publicated || blog.BlogStatus == BlogStatus.rejected) )
                )
                ) { 
                return View(blog);
            }
            var message = "You can not access the blog.";
            return RedirectToPage("Blog/Index", new { error = message });

        }

        [SessionActionFilter]
        // GET: BlogController/Create
        public ActionResult Create() {
            return View();
        }

        // POST: BlogController/Create
        [HttpPost]
        [SessionActionFilter]
        public ActionResult PostBlog([FromBody] Blog newBlog) {
            try {
                var user = this._GetUserFromSession();

                if(_blogPublication.PostBlog(user, newBlog)) {
                    return RedirectToAction(nameof(Index));
                }
                else {
                    var message = "There has been an error posting the blog.";
                    return RedirectToPage("Blog/Index", new { error = message });
                }
            }
            catch(Exception ex) {
                _logger.LogError($"Error at creating Blog. {ex.Message}. {ex.GetType().FullName} . {ex.StackTrace}");
                var message = "ErrorPlease try latter";
                ViewData["ErrorMessage"] = message;
                return View();
            }
        }

        // GET: BlogController/Edit/5

        [SessionActionFilter]
        public ActionResult Edit(long id) {
            var user = this._GetUserFromSession();
            var blog = _blogPublication.GetBlogById(id, user);
            if( blog != null &&
                user.Role.RolType != RolType.editor &&
                blog.Author.Id == user.Id &&
                blog.BlogStatus == BlogStatus.rejected) { 
                return View(blog);
            }
            var message = "You can not access the blog.";
            return RedirectToPage("Blog/Index", new { error = message });
        }

        [SessionActionFilter]
        public ActionResult RejectBlog(long id) {
            var user = this._GetUserFromSession();
            var blog = _blogPublication.GetBlogById(id, user);
            if(blog != null &&
                user.Role.RolType == RolType.editor &&
                blog.BlogStatus != BlogStatus.rejected) {
                _blogPublication.RejectBlog(user,blog);
                return View(blog);
            }
            var message ="You can not access the blog.";
            return RedirectToPage("Blog/Index", new { error = message });
        }

        // POST: BlogController/Edit/5
        [HttpPost]
        [SessionActionFilter]
        public ActionResult Filter(int id, [FromBody] BlogFilterParameters parameters) {
            try {
                /*
                I will not implement this method because of the time.
                Basically I wanted to show that I much rather use an object instead of multiple parameters.

                If I had implemented it I would have written the query directly and set to the Entity FrameWork.
                Beacuse by writting I can use hints like with(nolock) o with(readuncommited) etc.

                And finally this should return a list in json format
                 */


                return RedirectToAction(nameof(Filter));
            }
            catch {
                return View();
            }
        }

        // GET: BlogController/Delete/5
        [SessionActionFilter]
        public ActionResult Delete(long id) {
            var user = this._GetUserFromSession();
            var blog = _blogPublication.GetBlogById(id, user);
            if(blog != null &&
                user.Role.RolType == RolType.editor &&
                blog.BlogStatus != BlogStatus.deleted) {
                _blogPublication.DeleteBlog(user, blog);
                return View(blog);
            }
            var message = "You can not access the blog.";
            return RedirectToPage("Blog/Index", new { error = message });
        }

      
    }
}
