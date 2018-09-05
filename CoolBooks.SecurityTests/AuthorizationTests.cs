using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoolBooks.Models;

namespace CoolBooks.SecurityTests
{
    [TestFixture]
    public class AuthorizationTests
    {
        [TestCase ("Test1@test.se",1, "Test2@test.se")]
        [TestCase("Test2@test.se", 2, "Test4@test.se")]
        [TestCase("Test3@test.se", 3, "Test5@test.se")]
        [TestCase("Valid@test.se", 5, "NotValido@test.se")]
        [TestCase("Good@test.se", 42, "NoGood@test.se")]
        public void ShouldReturnFalseOnNotAuthorizedToEditBook(string userName,int bookId,string bookUserId)
        {
            var fakeUser = new Mock<System.Security.Principal.IPrincipal>();
            var fakeAuthentication = new Mock<IAuthentication>();
            fakeAuthentication.Setup(x => x.GetUserId(fakeUser.Object)).Returns(userName);
            fakeUser.Setup(f => f.IsInRole("Admin")).Returns(false);
            Books book =
            new Books
            {
              Title = "Test title 1",
              Id = bookId,
              ISBN = "12312432432",
            };

            var sut = new Authorization(fakeAuthentication.Object);

            var result = sut.IsAuthorizedToEditBook(book, fakeUser.Object);

            Assert.That(result, Is.EqualTo(false));
        }

        [TestCase("Test1@test.se", 1, "Test1@test.se")]
        [TestCase("Test2@test.se", 2, "Test2@test.se")]
        [TestCase("Test3@test.se", 3, "Test3@test.se")]
        [TestCase("Valid@test.se", 5, "Valid@test.se")]
        [TestCase("Good@test.se", 42, "Good@test.se")]
        public void ShouldReturnTrueOnNotAuthorizedToEditBook(string userName, int bookId, string bookUserId)
        {
            var fakeUser = new Mock<System.Security.Principal.IPrincipal>();
            var fakeAuthentication = new Mock<IAuthentication>();
            fakeAuthentication.Setup(x => x.GetUserId(fakeUser.Object)).Returns(userName);
            fakeUser.Setup(f => f.IsInRole("Admin")).Returns(false);
            Books book =
            new Books
            {
                Title = "Test title 1",
                Id = bookId,
                ISBN = "12312432432",
            };

            var sut = new Authorization(fakeAuthentication.Object);

            var result = sut.IsAuthorizedToEditBook(book, fakeUser.Object);

            Assert.That(result, Is.EqualTo(false));
        }

        [TestCase("Test1@test.se", 1, "Test2@test.se")]
        [TestCase("Test2@test.se", 2, "Test4@test.se")]
        [TestCase("Test3@test.se", 3, "TestNot@test.se")]
        [TestCase("Valid@test.se", 5, "NotValid@test.se")]
        [TestCase("Good@test.se", 42, "NoGood@test.se")]
        public void ShouldReturnTrueOnNotAuthorizedToEditBookByAdmin(string userName, int bookId, string bookUserId)
        {
            var fakeUser = new Mock<System.Security.Principal.IPrincipal>();
            var fakeAuthentication = new Mock<IAuthentication>();
            fakeAuthentication.Setup(x => x.GetUserId(fakeUser.Object)).Returns(userName);
            fakeUser.Setup(f => f.IsInRole("Admin")).Returns(true);
            Books book =
            new Books
            {
                Title = "Test title 1",
                Id = bookId,
                ISBN = "12312432432",
            };

            var sut = new Authorization(fakeAuthentication.Object);

            var result = sut.IsAuthorizedToEditBook(book, fakeUser.Object);

            Assert.That(result, Is.EqualTo(true));
        }

        [TestCase("Test1@test.se", 1, "Test2@test.se")]
        [TestCase("Test2@test.se", 2, "Test4@test.se")]
        [TestCase("Test3@test.se", 3, "Test5@test.se")]
        [TestCase("Valid@test.se", 5, "NotValido@test.se")]
        [TestCase("Good@test.se", 42, "NoGood@test.se")]
        public void ShouldReturnFalseOnNotAuthorizedToDeleteBook(string userName, int bookId, string bookUserId)
        {
            var fakeUser = new Mock<System.Security.Principal.IPrincipal>();
            var fakeAuthentication = new Mock<IAuthentication>();
            fakeAuthentication.Setup(x => x.GetUserId(fakeUser.Object)).Returns(userName);
            fakeUser.Setup(f => f.IsInRole("Admin")).Returns(false);
            Books book =
            new Books
            {
                Title = "Test title 1",
                Id = bookId,
                ISBN = "12312432432",
            };

            var sut = new Authorization(fakeAuthentication.Object);

            var result = sut.IsAuthorizedToDeleteBook(book, fakeUser.Object);

            Assert.That(result, Is.EqualTo(false));
        }

        [TestCase("Test1@test.se", 1, "Test1@test.se")]
        [TestCase("Test2@test.se", 2, "Test2@test.se")]
        [TestCase("Test3@test.se", 3, "Test3@test.se")]
        [TestCase("Valid@test.se", 5, "Valid@test.se")]
        [TestCase("Good@test.se", 42, "Good@test.se")]
        public void ShouldReturnTrueOnNotAuthorizedToDeleteBook(string userName, int bookId, string bookUserId)
        {
            var fakeUser = new Mock<System.Security.Principal.IPrincipal>();
            var fakeAuthentication = new Mock<IAuthentication>();
            fakeAuthentication.Setup(x => x.GetUserId(fakeUser.Object)).Returns(userName);
            fakeUser.Setup(f => f.IsInRole("Admin")).Returns(false);
            Books book =
            new Books
            {
                Title = "Test title 1",
                Id = bookId,
                ISBN = "12312432432",
            };

            var sut = new Authorization(fakeAuthentication.Object);

            var result = sut.IsAuthorizedToDeleteBook(book, fakeUser.Object);

            Assert.That(result, Is.EqualTo(false));
        }

        [TestCase("Test1@test.se", 1, "Test2@test.se")]
        [TestCase("Test2@test.se", 2, "Test4@test.se")]
        [TestCase("Test3@test.se", 3, "TestNot@test.se")]
        [TestCase("Valid@test.se", 5, "NotValid@test.se")]
        [TestCase("Good@test.se", 42, "NoGood@test.se")]
        public void ShouldReturnTrueOnNotAuthorizedToDeleteBookByAdmin(string userName, int bookId, string bookUserId)
        {
            var fakeUser = new Mock<System.Security.Principal.IPrincipal>();
            var fakeAuthentication = new Mock<IAuthentication>();
            fakeAuthentication.Setup(x => x.GetUserId(fakeUser.Object)).Returns(userName);
            fakeUser.Setup(f => f.IsInRole("Admin")).Returns(true);
            Books book =
            new Books
            {
                Title = "Test title 1",
                Id = bookId,
                ISBN = "12312432432",
            };

            var sut = new Authorization(fakeAuthentication.Object);

            var result = sut.IsAuthorizedToDeleteBook(book, fakeUser.Object);

            Assert.That(result, Is.EqualTo(true));
        }
    }
}
