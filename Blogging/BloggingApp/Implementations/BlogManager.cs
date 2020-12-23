using BloggingApp.Data;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingApp.Implementations {
    /// <summary>
    /// this class will manage the entity fw connection
    /// </summary>
    public class BlogManager : IBlogPublication, IBlogPublicationFetcher {

        private readonly BloggingAppContext _context;
        public BlogManager(IConfiguration Configuration) {
            DbContextOptionsBuilder<BloggingAppContext> builder = new DbContextOptionsBuilder<BloggingAppContext>();
            builder.UseSqlServer(Configuration.GetConnectionString("BloggingAppContext"));
            _context = new BloggingAppContext(builder.Options);
         }

        public bool DeleteBlog(User user, Blog blog) {
            bool result = true;
            try {
                if(user == null)
                    throw new ArgumentNullException("user parameter can not be null");
                if(user.Role.RolType != RolType.editor)
                    throw new ArgumentException("Only Editors can delete blogs.");
                _context.Remove(blog);
                _context.SaveChanges();
            }
            catch(Exception ) {
                throw;
            }
            return result;
        }

        public Blog GetBlogById(long id, User user) {
            Blog result = null;
            try {
                result =  _context.Blog.FirstOrDefault(p=> p.Id == id);
            }
            catch(Exception ) {
                throw;
            }
            return result;
        }

        public IEnumerable<Blog> GetBlogs(User user) {
            IEnumerable<Blog> result = null;
            try {
                result =  _context.Blog.Where(p => p.BlogStatus == BlogStatus.publicated);
            }
            catch(Exception ) {
                throw;
            }
            return result;
        }

        public IEnumerable<Blog> GetBlogs(User user, IBlogFilterParameters parameters) {
            IEnumerable<Blog> result = null;
            try {
                ///this is the complex implementation I said i would not implement
            }
            catch(Exception ) {
                throw;
            }
            return result;
        }

        public IEnumerable<Blog> GetPenddingBlogs(User user) {
            IEnumerable<Blog> result = null;
            try {
                if(user == null)
                    throw new ArgumentNullException("user parameter can not be null");
                if(user.Role.RolType != RolType.editor)
                    throw new ArgumentException("Only Editors can see Pendding blogs.");
                result =  _context.Blog.Where(p => p.BlogStatus == BlogStatus.pendingPublishApproval && p.Author.Id != user.Id);
            }
            catch(Exception ) {
                throw;
            }
            return result;
        }

        public IEnumerable<Blog> GetPenddingBlogs(User user, IBlogFilterParameters parameters) {
            IEnumerable<Blog> result = null;
            try {
                ///this is the complex implementation I said i would not implement
            }
            catch(Exception ) {
                throw;
            }
            return result;
        }

        public bool PostBlog(User user, Blog blog) {
            bool result = false;
            try {
                if(user == null)
                    throw new ArgumentNullException("user parameter can not be null");
                blog.Author = user;
                blog.BlogStatus = BlogStatus.pendingPublishApproval;

                _context.Add(blog);
                _context.SaveChanges();
            }
            catch(Exception ) {
                throw;
            }
            return result;
        }

        public bool PublishBlog(User user, Blog blog) {
            bool result = false;
            try {
                if(user == null)
                    throw new ArgumentNullException("user parameter can not be null.");
                if(user.Role.RolType != RolType.editor)
                    throw new ArgumentException("Only Editors can approved blogs.");
                blog.BlogStatus = BlogStatus.publicated;
                blog.EditorUser = user;
                _context.Update(blog);
                _context.SaveChanges();
            }
            catch(Exception ) {
                throw;
            }
            return result;
        }

        public void RejectBlog(User user, Blog blog) {
            try {
                if(user == null)
                    throw new ArgumentNullException("user parameter can not be null");
                if(user.Role.RolType != RolType.editor)
                    throw new ArgumentException("Only Editors can reject blogs.");
                blog.BlogStatus = BlogStatus.rejected;
                blog.EditorUser = user;
                _context.Update(blog);
                _context.SaveChanges();
            }
            catch(Exception ) {
                throw;
            }

        }
    }
}
