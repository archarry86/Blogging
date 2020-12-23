using BloggingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingApp.Interfaces {
    public interface IBlogPublication {
        /// <summary>
        /// A user post a blog in the site.
        /// The state of the blog become pending to aproval
        /// Even if the user that has posted the blog has the Editor Rol
        /// </summary>
        /// <param name="user"></param>
        /// <param name="blog"></param>
        /// <returns></returns>
        bool PostBlog(User user, Blog blog);
        /// <summary>
        /// An Editor publish the blog
        /// </summary>
        /// <param name="user"></param>
        /// <param name="blog"></param>
        /// <returns></returns>
        bool PublishBlog(User user, Blog blog);
        /// <summary>
        /// An Editor publish the blog
        /// </summary>
        /// <param name="user"></param>
        /// <param name="blog"></param>
        /// <returns></returns>
        bool DeleteBlog(User user, Blog blog);
        /// <summary>
        /// An Editor publish the blog
        /// </summary>
        /// <param name="user"></param>
        /// <param name="blog"></param>
        /// <returns></returns>
        IEnumerable<Blog> GetPenddingBlogs(User user);
   
        /// <summary>
        /// An Editor publish the blog
        /// </summary>
        /// <param name="user"></param>
        /// <param name="blog"></param>
        /// <returns></returns>
        IEnumerable<Blog> GetBlogs(User user);


    }
}
