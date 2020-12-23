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
using System.Text.Json;

namespace UnitTestProject {
    [TestClass]
    public class UnitTestBlogController {
        [TestMethod]
        public void TestIndex() {
            // Creamos el mock sobre nuestra interfaz
            var usersCollection = UserCollection();
            var loggerMock = new Mock<ILogger<BlogController>>();
            var sessionMock = Mocksession(usersCollection[0]);
            var BlogPublication = CreateMockIBlogPublication();
            var BlogPublicationFetcher = CreateMockIBlogPublicationFetcher();
            var controller = new BlogController(loggerMock.Object,
              BlogPublication.Object,
              BlogPublicationFetcher.Object
              );
            controller.SetISession(sessionMock.Object);
            var iactionresult = controller.Index();
            Assert.IsInstanceOfType(iactionresult, typeof(ViewResult));
            ViewResult viewResult = iactionresult as ViewResult;
            Assert.IsTrue(viewResult.Model is IEnumerable<Blog>);
        }

        [TestMethod]
        public void DetailsBlogDoesnotExits() {
            // Creamos el mock sobre nuestra interfaz
            var usersCollection = UserCollection();
            var loggerMock = new Mock<ILogger<BlogController>>();
            var sessionMock = Mocksession(usersCollection[0]);
            var BlogPublication = CreateMockIBlogPublication();
            var BlogPublicationFetcher = CreateMockIBlogPublicationFetcher();
            var controller = new BlogController(loggerMock.Object,
                      BlogPublication.Object,
                      BlogPublicationFetcher.Object
                      );
            controller.SetISession(sessionMock.Object);
            controller.SetUser(usersCollection[0]);
            var idblog = 0l;
            var iactionresult = controller.Details(idblog);
            Assert.IsInstanceOfType(iactionresult, typeof(RedirectToPageResult));
            RedirectToPageResult redirectToPageResult = iactionresult as RedirectToPageResult;
            Assert.IsTrue(redirectToPageResult.PageName.Contains("Blog/Index?error="));
        }

        [TestMethod]
        public void DetailsEditor() {
            // Creamos el mock sobre nuestra interfaz
            var usersCollection = UserCollection();
            var loggerMock = new Mock<ILogger<BlogController>>();
            var sessionMock = Mocksession(usersCollection[0]);
            var BlogPublication = CreateMockIBlogPublication();
            var BlogPublicationFetcher = CreateMockIBlogPublicationFetcher();
            var controller = new BlogController(loggerMock.Object,
                BlogPublication.Object,
                BlogPublicationFetcher.Object
                );
            controller.SetISession(sessionMock.Object);
            controller.SetUser(usersCollection[0]);
            long idblog = 1;
            var iactionresult = controller.Details(idblog);
            Assert.IsInstanceOfType(iactionresult, typeof(ViewResult));
            ViewResult viewResult = iactionresult as ViewResult;
            Assert.IsTrue(viewResult.Model is Blog);
        }

        [TestMethod]
        public void BlogDetailsUserCannotaccess() {
            // Creamos el mock sobre nuestra interfaz
            var usersCollection = UserCollection();
            var loggerMock = new Mock<ILogger<BlogController>>();
            var sessionMock = Mocksession(usersCollection[0]);
            var BlogPublication = CreateMockIBlogPublication();
            var BlogPublicationFetcher = CreateMockIBlogPublicationFetcher();
            var controller = new BlogController(loggerMock.Object,
               BlogPublication.Object,
               BlogPublicationFetcher.Object
               );
            controller.SetISession(sessionMock.Object);
            //uuser that is not an editor
            controller.SetUser(usersCollection[1]);
            long idblog = 0;
            var iactionresult = controller.Details(idblog);
            Assert.IsInstanceOfType(iactionresult, typeof(RedirectToPageResult));
            RedirectToPageResult redirectToPageResult = iactionresult as RedirectToPageResult;
            Assert.IsTrue(redirectToPageResult.PageName.Contains("Blog/Index?error="));
        }


        [TestMethod]
        public void PublishBlog() {
            // Creamos el mock sobre nuestra interfaz
            var usersCollection = UserCollection();
            var loggerMock = new Mock<ILogger<BlogController>>();
            var sessionMock = Mocksession(usersCollection[0]);
            var BlogPublication = CreateMockIBlogPublication();
            var BlogPublicationFetcher = CreateMockIBlogPublicationFetcher();
            var controller = new BlogController(loggerMock.Object,
               BlogPublication.Object,
               BlogPublicationFetcher.Object
               );
            controller.SetISession(sessionMock.Object);
            //user that is not an editor
            controller.SetUser(usersCollection[1]);
            long idblog = 0;

            var iactionresult = controller.PostBlog(new Blog() {
                Id = 4,
                ApprovalTime = DateTime.MinValue,
                Author = null,
                BlogStatus = BlogStatus._none,
                Content = "Blog content new ...",
                EditorUser = null,
                FingerPrintUser = "Data of the connection",
                PublicationTime = DateTime.Now,
                Title = "new  test of the  blog",
                Topic = "New mocking references"


            });
            Assert.IsInstanceOfType(iactionresult, typeof(RedirectToPageResult));
          
        }



        [TestMethod]
        public void UserCannotEditBlog() {
          
            var usersCollection = UserCollection();
            var loggerMock = new Mock<ILogger<BlogController>>();
            var sessionMock = Mocksession(usersCollection[1]);
            var BlogPublication = CreateMockIBlogPublication();
            var BlogPublicationFetcher = CreateMockIBlogPublicationFetcher();
            var controller = new BlogController(loggerMock.Object,
                BlogPublication.Object,
                BlogPublicationFetcher.Object
                );
            controller.SetISession(sessionMock.Object);
            controller.SetUser(usersCollection[1]);
            //este blog not tiene un estado valido paraser editado
            long idblog = 2;
            var iactionresult = controller.Edit(idblog);
            Assert.IsInstanceOfType(iactionresult, typeof(RedirectToPageResult));
            RedirectToPageResult redirectToPageResult = iactionresult as RedirectToPageResult;
            Assert.IsTrue(redirectToPageResult.PageName.Contains("Blog/Index?error="));
        }


        [TestMethod]
        public void UserEditBlog() {
            // Creamos el mock sobre nuestra interfaz
            var usersCollection = UserCollection();
            var loggerMock = new Mock<ILogger<BlogController>>();
            var sessionMock = Mocksession(usersCollection[1]);
            var BlogPublication = CreateMockIBlogPublication();
            var BlogPublicationFetcher = CreateMockIBlogPublicationFetcher();
            var controller = new BlogController(loggerMock.Object,
               BlogPublication.Object,
               BlogPublicationFetcher.Object
               );
            controller.SetISession(sessionMock.Object);

            controller.SetUser(usersCollection[1]);
            //este blog puede ser editado
            long idblog = 3;
            var iactionresult = controller.Edit(idblog);
            Assert.IsInstanceOfType(iactionresult, typeof(ViewResult));
            ViewResult viewResult = iactionresult as ViewResult;
        }


        [TestMethod]
        public void EditorRejectBlog() {
            // Creamos el mock sobre nuestra interfaz
            var usersCollection = UserCollection();
            var loggerMock = new Mock<ILogger<BlogController>>();
            var sessionMock = Mocksession(usersCollection[1]);
            var BlogPublication = CreateMockIBlogPublication();
            var BlogPublicationFetcher = CreateMockIBlogPublicationFetcher();
            var controller = new BlogController(loggerMock.Object,
              BlogPublication.Object,
              BlogPublicationFetcher.Object
              );
            controller.SetISession(sessionMock.Object);
            //efitor user is assing
            controller.SetUser(usersCollection[0]);
            //este blog puede ser editado
            long idblog = 1;
            var iactionresult = controller.RejectBlog(idblog);
            Assert.IsInstanceOfType(iactionresult, typeof(ViewResult));
            ViewResult viewResult = iactionresult as ViewResult;
        }


        [TestMethod]
        public void EditorDeleteBlog() {
            // Creamos el mock sobre nuestra interfaz
            var usersCollection = UserCollection();
            var loggerMock = new Mock<ILogger<BlogController>>();
            var sessionMock = Mocksession(usersCollection[1]);
            var BlogPublication = CreateMockIBlogPublication();
            var BlogPublicationFetcher = CreateMockIBlogPublicationFetcher();
            var controller = new BlogController(loggerMock.Object,
                  BlogPublication.Object,
                  BlogPublicationFetcher.Object
                  );
            controller.SetISession(sessionMock.Object);
            //efitor user is assing
            controller.SetUser(usersCollection[0]);
            //este blog puede ser editado
            long idblog = 1;
            var iactionresult = controller.Delete(idblog);
            Assert.IsInstanceOfType(iactionresult, typeof(ViewResult));
            ViewResult viewResult = iactionresult as ViewResult;
        }

        private Mock<ISession> Mocksession(User loggedUser) {

            var sessionMock = new Mock<ISession>();



            return sessionMock;
        }

        private Mock<IBlogPublication> CreateMockIBlogPublication() {
            var IBlogPublication = new Mock<IBlogPublication>();
            var list = BlogCollection();
            IBlogPublication.Setup(m => m.GetBlogById(It.IsAny<long>(), It.IsAny<User>())).Returns(
                (long id, User user) => {

                    return list.FirstOrDefault(P => P.Id == id);
                }

                );


            return IBlogPublication;
        }

        private Mock<IBlogPublicationFetcher> CreateMockIBlogPublicationFetcher() {
            var IBlogPublicationFetcher = new Mock<IBlogPublicationFetcher>();


            return IBlogPublicationFetcher;
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


        private List<Blog> BlogCollection() {
            List<Blog> Blog = new List<Blog>() {
                new Blog(){
               Id = 1,
               ApprovalTime= DateTime.MinValue,
               Author =    new User() {
                    Id = 2,
                    Login = "ablogger",
                    Password = "12345",
                    Role = new Role() {
                        Id = 1,
                        RolType = RolType.user
                    }
                },
               BlogStatus = BlogStatus.pendingPublishApproval,
               Content="Blog content ...",
               EditorUser= null,
               FingerPrintUser="Data of the connection",
               PublicationTime = DateTime.MinValue,
               Title="test of the  blog",
               Topic="Mocking references"


                },
                    new Blog(){
               Id = 2,
               ApprovalTime= DateTime.MinValue,
               Author =    new User() {
                    Id = 2,
                    Login = "ablogger",
                    Password = "12345",
                    Role = new Role() {
                        Id = 1,
                        RolType = RolType.user
                    }
                },
               BlogStatus = BlogStatus.publicated,
               Content="Blog content 2...",
               EditorUser= null,
               FingerPrintUser="Data of the connection",
               PublicationTime = DateTime.MinValue,
               Title="test of the  blog",
               Topic="Mocking references"


                },      new Blog(){
               Id = 3,
               ApprovalTime= DateTime.MinValue,
               Author =    new User() {
                    Id = 2,
                    Login = "ablogger",
                    Password = "12345",
                    Role = new Role() {
                        Id = 1,
                        RolType = RolType.user
                    }
                },
               BlogStatus = BlogStatus.rejected,
               Content="Blog content 2...",
               EditorUser=  new User() {
                    Id = 1,
                    Login = "admin",
                    Password = "12345",
                    Role = new Role() {
                        Id = 1,
                        RolType = RolType.editor
                    }
                },
               FingerPrintUser="Data of the connection",
               PublicationTime = DateTime.MinValue,
               Title="test of the  blog",
               Topic="Mocking references"


                }
            };

            return Blog;
        }
    }
}
