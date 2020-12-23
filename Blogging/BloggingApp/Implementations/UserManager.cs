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
    /// this class will manage the entity fw object
    /// </summary>
    public class UserManager : IUserManager {

        private readonly BloggingAppContext _context;
        public UserManager(IConfiguration Configuration) {

            DbContextOptionsBuilder<BloggingAppContext> builder = new DbContextOptionsBuilder<BloggingAppContext>();
            builder.UseSqlServer(Configuration.GetConnectionString("BloggingAppContext"));
            _context = new BloggingAppContext(builder.Options);
        }

        public User CreateAnonymousLogin() {
            //te user with id zero must be created on the database
            User anonymous = new User();
            anonymous.Id = 0;
            anonymous.Login = "Anonymous";
            return anonymous;
        }

        public User ValidateUser(string login, string password) {
            User result = null;
            try {
                result =  _context.User.FirstOrDefault(p => p.Login == login && p.Password == password);
                //the porperty Password was added to call the method FirstOrDefault however
                //I do not want to heep the password in the session
                if(result != null)
                result.Password = "";
            }
            catch(Exception ) {
                throw;
            }
            return result;
        }
    }
}
