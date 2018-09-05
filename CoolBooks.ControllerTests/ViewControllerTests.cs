using Moq;
using NUnit.Framework;
using System.Net;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;
using CoolBooks.Models;
using CoolBooks.Controllers;
using CoolBooks;
using System.Collections.Generic;
using CoolBooks.Repositories;

namespace CoolBooks.ControllerTests
{
    [TestFixture]
    public class ViewControllerTests
    {

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(10)]
        [TestCase(42)]
        public void ShouldReturnDefaultDetailsView(int bookId)
        {
            var fakeViewRepo = new Mock<IViewFacade>();
            var fakeAuthentication = new Mock<IAuthentication>();
            var fakeAuhtorization = new Mock<IAuthorization>();

            Books book =
                new Books
                {
                    Title = "Test title 1",
                    Id = bookId,
                    ISBN = "12312432432",
                };

            fakeViewRepo.Setup(r => r.GetBookById(bookId)).Returns(book);


            var sut = new ViewController(fakeAuthentication.Object,fakeAuhtorization.Object,fakeViewRepo.Object);

            var result = sut.WithCallTo(x => x.Index(bookId,"")).ShouldRenderDefaultView().WithModel(book);
        }


        [Test]
        public void ShouldReturnHtttpStatusCodeBadRequest()
        {

            var fakeRepo = new Mock<IViewFacade>();
            var fakeAuthentication = new Mock<IAuthentication>();
            var fakeAuhtorization = new Mock<IAuthorization>();


            var sut = new ViewController(fakeAuthentication.Object, fakeAuhtorization.Object, fakeRepo.Object);
            var result = sut.WithCallTo(x => x.Index((int?)null,"")).ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [TestCase(1)]
        [TestCase(12)]
        [TestCase(14)]
        public void ShouldReturnHtttpStatusCodeNotFound(int bookId)
        {

            var fakeRepo = new Mock<IViewFacade>();
            var fakeAuthentication = new Mock<IAuthentication>();
            var fakeAuhtorization = new Mock<IAuthorization>();


            fakeRepo.Setup(r => r.GetBookById(bookId)).Returns((Books)null);

            var sut = new ViewController(fakeAuthentication.Object, fakeAuhtorization.Object, fakeRepo.Object);

            var result = sut.WithCallTo(x => x.Index((int?)null,"")).ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }
    }
}
