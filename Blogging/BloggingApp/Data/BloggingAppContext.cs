using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BloggingApp.Models;

namespace BloggingApp.Data
{
    public class BloggingAppContext : DbContext
    {
        public BloggingAppContext (DbContextOptions<BloggingAppContext> options)
            : base(options)
        {
        }

        public DbSet<BloggingApp.Models.Blog> Blog { get; set; }

        public DbSet<BloggingApp.Models.User> User { get; set; }
    }
}
