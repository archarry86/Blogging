using BloggingApp.Interfaces;
using BloggingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingApp.Implementations {
    /// <summary>
    /// this class will manage the entity fw connection
    /// </summary>
    public class BlogManager : IBlogPublication, IBlogPublicationFetcher {
        public bool DeleteBlog(User user, Blog blog) {
            bool result = true;
            try {

            }
            catch(Exception ex) {
                throw;
            }
            return result;
        }

        public Blog GetBlogById(long id, User user) {
            Blog result = null;
            try {

            }
            catch(Exception ex) {
                throw;
            }
            return result;
        }

        public IEnumerable<Blog> GetBlogs(User user) {
            IEnumerable<Blog> result = null;
            try {

            }
            catch(Exception ex) {
                throw;
            }
            return result;
        }

        public IEnumerable<Blog> GetBlogs(User user, IBlogFilterParameters parameters) {
            IEnumerable<Blog> result = null;
            try {

            }
            catch(Exception ex) {
                throw;
            }
            return result;
        }

        public IEnumerable<Blog> GetPenddingBlogs(User user) {
            IEnumerable<Blog> result = null;
            try {

            }
            catch(Exception ex) {
                throw;
            }
            return result;
        }

        public IEnumerable<Blog> GetPenddingBlogs(User user, IBlogFilterParameters parameters) {
            IEnumerable<Blog> result = null;
            try {

            }
            catch(Exception ex) {
                throw;
            }
            return result;
        }

        public bool PostBlog(User user, Blog blog) {
            bool result = false;
            try {

            }
            catch(Exception ex) {

            }
            return result;
        }

        public bool PublishBlog(User user, Blog blog) {
            bool result = false;
            try {

            }
            catch(Exception ex) {
                throw;
            }
            return result;
        }

        public void RejectBlog(Blog blog) {
            
            try {

            }
            catch(Exception ex) {
                throw;
            }
           
        }
    }
}
