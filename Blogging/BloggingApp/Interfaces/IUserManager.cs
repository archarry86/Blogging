using BloggingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingApp.Interfaces {
    public interface IUserManager {
        /// <summary>
        /// Retruns an user if exits otherwise throws an exception
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        User ValidateUser(String login, String password);
        /// <summary>
        /// Retruns an anonimous user
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        User CreateAnonymousLogin();



    }
}
