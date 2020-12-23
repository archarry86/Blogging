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
    public class UserManager : IUserManager {
        public User CreateAnonymousLogin() {
            User anonymous = new User();
            return anonymous;
                
        }

        public User ValidateUser(string login, string password) {
            User result = null;
            try {

            }
            catch(Exception ex) {
                throw;
            }
            return result;
        }
    }
}
