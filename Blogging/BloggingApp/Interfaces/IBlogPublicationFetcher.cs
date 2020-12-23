using BloggingApp.Models;
using BloggingApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingApp.Interfaces {
    /// <summary>
    /// Interface to intends show knowledge of interface segregation
    /// </summary>
    public interface IBlogPublicationFetcher {
       
        /// <summary>
        /// An Editor publish the blog
        /// </summary>
        /// <param name="user"></param>
        /// <param name="blog"></param>
        /// <returns></returns>
        IEnumerable<Blog> GetPenddingBlogs(User user, IBlogFilterParameters parameters);

        /// <summary>
        /// An Editor publish the blog
        /// </summary>
        /// <param name="user"></param>
        /// <param name="blog"></param>
        /// <returns></returns>
        IEnumerable<Blog> GetBlogs(User user, IBlogFilterParameters parameters);

    }
}
