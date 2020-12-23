using Moq;
using BloggingApp.Interfaces;
using BloggingApp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Logging;
using BloggingApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace UnitTestProject {
    [TestClass]
    public class UnitTestLoginController {
        [TestMethod]
        public void TestValidateEditorUser() {
            // Creamos el mock sobre nuestra interfaz
            var usersCollection = UserCollection();
            var loggerMock = new Mock<ILogger<LoginController>>();
            var sessionMock = new Mock<ISession>();
            var userMock = CreateMock();
            var controller = new LoginController( loggerMock.Object, userMock.Object, sessionMock.Object);
            var iactionresult =  controller.Login(new BloggingApp.ViewModel.ModelWiewUser() {
                Login="admin",
                Password = "12345",
            });
            Assert.IsInstanceOfType(iactionresult, typeof(RedirectToPageResult));
            RedirectToPageResult redirectToPageResult = iactionresult as RedirectToPageResult;
            Assert.IsTrue(redirectToPageResult.PageName.Contains("Blog/Index"));
        }


        [TestMethod]
        public void TestValidateUser() {
            // Creamos el mock sobre nuestra interfaz
            var usersCollection = UserCollection();
            var loggerMock = new Mock<ILogger<LoginController>>();
            var sessionMock = new Mock<ISession>();
            var userMock = CreateMock();
            var controller = new LoginController(loggerMock.Object, userMock.Object, sessionMock.Object);
            var iactionresult = controller.Login(new BloggingApp.ViewModel.ModelWiewUser() {
                Login = "ablogger",
                Password = "12345",
            });
            Assert.IsInstanceOfType(iactionresult, typeof(RedirectToPageResult));
            RedirectToPageResult redirectToPageResult = iactionresult as RedirectToPageResult;
            Assert.IsTrue(redirectToPageResult.PageName.Contains("Blog/Index"));
        }

        [TestMethod]
        public void TestValidateUserDoesnotExits() {
            var usersCollection = UserCollection();
            var loggerMock = new Mock<ILogger<LoginController>>();
            var userMock = CreateMock();
            var sessionMock = new Mock<ISession>();
            var controller = new LoginController(loggerMock.Object, userMock.Object, sessionMock.Object);
            var iactionresult = controller.Login(new BloggingApp.ViewModel.ModelWiewUser() {
                Login = "invalidyser",
                Password = "12345",
            });
            Assert.IsInstanceOfType(iactionresult, typeof(RedirectToPageResult));
            RedirectToPageResult redirectResult = iactionresult as RedirectToPageResult;
            Assert.IsTrue(redirectResult.PageName.Contains("Home/Index?error"));
        }

        [TestMethod]
        public void TestAnonymousLogin() {
            // Creamos el mock sobre nuestra interfaz
            var usersCollection = UserCollection();
            var loggerMock = new Mock<ILogger<LoginController>>();
            var sessionMock = new Mock<ISession>();
            var userMock = CreateMock();
            var controller = new LoginController(loggerMock.Object, userMock.Object, sessionMock.Object);
            var iactionresult = controller.AnonymousLogin();
            Assert.IsInstanceOfType(iactionresult, typeof(RedirectToPageResult));
            RedirectToPageResult redirectResult = iactionresult as RedirectToPageResult;
            Assert.IsTrue(redirectResult.PageName.Contains("Blog/Index"));
        }

        private Mock<IUserManager> CreateMock() {
            var userMock = new Mock<IUserManager>();
            var users = UserCollection();
            // Definimos el comportamiento del método GetCount y su resultado
            userMock.Setup(m => m.ValidateUser(It.IsAny<string>(), It.IsAny<string>())).Returns((string login, string password) => {
                return users.FirstOrDefault(p => p.Login == login && p.Password == password);
            });
            userMock.Setup(m => m.ValidateUser(null, null)).Throws<ArgumentNullException>();
            return userMock;
        }

        private List<User> UserCollection() {
            List<User> users = new List<User>() { 
                new User() {
                    Id = 1,
                    Login = "admin",
                    Password = "12345",
                    Role = new Role() {
                        Id = 1,
                        RolType = RolType.editor
                    }
                },
                new User() {
                    Id = 2,
                    Login = "ablogger",
                    Password = "12345",
                    Role = new Role() {
                        Id = 1,
                        RolType = RolType.user
                    }
                },
                new User() {
                    Id = 0,
                    Login = "Anonymous",
                    Password = "",
                    Role = new Role() {
                        Id = 0,
                        RolType = RolType.anonymousUser
                    }
                }
            };

            return users;
        }
    }
}
