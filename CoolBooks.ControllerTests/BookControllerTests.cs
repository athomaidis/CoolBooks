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
    public class BookControllerTests
    {
        [Test]
        public void ShouldReturnDefaultIndexView()
        {
            var fakeBookRepo = new Mock<IBookFacade>();
            var fakeAuthentication = new Mock<IAuthentication>();
            var fakeAuhtorization = new Mock<IAuthorization>();

            List<Books> books = new List<Books>
            {
                new Books
                {
                    Title="Test title 1",
                    Id=1,
                    ISBN="12312432432",
                },
                new Books
                {
                    Title="Test title 2",
                    Id=2,
                    ISBN="11232432432",
                }
            };

            fakeBookRepo.Setup(r => r.Search("")).Returns(books);

            var sut = new BooksController(fakeAuthentication.Object,fakeAuhtorization.Object,fakeBookRepo.Object);

            var result = sut.WithCallTo(x => x.Index("")).ShouldRenderDefaultView().WithModel(books);  
        }

        [Test]
        public void ShouldReturnDefaultIndexViewAfterCallingSearch()
        {

            var fakeBookRepo = new Mock<IBookFacade>();
            var fakeAuthentication = new Mock<IAuthentication>();
            var fakeAuhtorization = new Mock<IAuthorization>();

            List<Books> books = new List<Books>
            {
                new Books
                {
                    Title="Test title 1 mySearchString",
                    Id=1,
                    ISBN="12312432432",
                },
                new Books
                {
                    Title="Test title 2 mySearchString",
                    Id=2,
                    ISBN="11232432432",
                }
            };

            fakeBookRepo.Setup(r => r.Search("mySearchString")).Returns(books);

            var sut = new BooksController(fakeAuthentication.Object, fakeAuhtorization.Object, fakeBookRepo.Object);

            var result = sut.WithCallTo(x => x.Index("mySearchString")).ShouldRenderDefaultView().WithModel(books);
        }


        [Test]
        public void ShouldReturnHtttpStatusCodeBadRequestOnEdit()
        {

            var fakeBookRepo = new Mock<IBookFacade>();
            var fakeAuthentication = new Mock<IAuthentication>();
            var fakeAuhtorization = new Mock<IAuthorization>();

            var sut = new BooksController(fakeAuthentication.Object, fakeAuhtorization.Object, fakeBookRepo.Object);

            var result = sut.WithCallTo(x => x.Edit((int?)null,"")).ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }
        [TestCase(1)]
        [TestCase(12)]
        [TestCase(14)]
        public void ShouldReturnHtttpStatusCodeNotFoundOnEdit(int bookId)
        {

            var fakeBookRepo = new Mock<IBookFacade>();
            var fakeAuthentication = new Mock<IAuthentication>();
            var fakeAuhtorization = new Mock<IAuthorization>();


            fakeBookRepo.Setup(r => r.GetById(bookId)).Returns((Books)null);

            var sut = new BooksController(fakeAuthentication.Object, fakeAuhtorization.Object, fakeBookRepo.Object);

            var result = sut.WithCallTo(x => x.Edit((int?)null,"")).ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(10)]
        [TestCase(42)]
        public void ShouldReturnHttpStatusForbidden(int bookId)
        {

            var fakeBookRepo = new Mock<IBookFacade>();
            var fakeAuthentication = new Mock<IAuthentication>();
            var fakeAuhtorization = new Mock<IAuthorization>();

            Books book =
                new Books
                {
                    Title = "Test title 1",
                    Id = bookId,
                    ISBN = "12312432432",
                };

            fakeBookRepo.Setup(r => r.GetByIdIncludingDeleted(bookId)).Returns(book);
            fakeAuhtorization.Setup(r => r.IsAuthorizedToEditBook(book, It.IsAny<System.Security.Principal.IPrincipal>())).Returns(false);

            var sut = new BooksController(fakeAuthentication.Object, fakeAuhtorization.Object, fakeBookRepo.Object);
            var result = sut.WithCallTo(x => x.Edit(bookId,"")).ShouldGiveHttpStatus(HttpStatusCode.Forbidden);
        }
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(10)]
        [TestCase(42)]
        public void ShouldReturnDefaultEditView(int bookId)
        {

            var fakeRepo = new Mock<IBookFacade>();
            var fakeAuthentication = new Mock<IAuthentication>();
            var fakeAuhtorization = new Mock<IAuthorization>();

            Books book =
                new Books
                {
                    Title = "Test title 1",
                    Id = bookId,
                    ISBN = "12312432432",
                };
            List<Authors> authors = new List<Authors>
            {
                new Authors
                {
                    FirstName="Oden",
                    LastName="Allfader",
                    Id =1
                },
                new Authors
                {
                    FirstName="Frej",
                    LastName="Njordsson",
                    Id =2
                }
            };
            List<Genres> genres = new List<Genres>
            {
                new Genres
                {
                    Name="Saga"
                },
                new Genres
                {
                    Name="Mythology"
                }
            };

            fakeRepo.Setup(r => r.GetByIdIncludingDeleted(bookId)).Returns(book);
            fakeRepo.Setup(r => r.GetAllAuthors()).Returns(authors);
            fakeRepo.Setup(r => r.GetAllGenres()).Returns(genres);
            fakeAuhtorization.Setup(r => r.IsAuthorizedToEditBook(book, It.IsAny<System.Security.Principal.IPrincipal>())).Returns(true);

            var sut = new BooksController(fakeAuthentication.Object, fakeAuhtorization.Object, fakeRepo.Object);

            var result = sut.WithCallTo(x => x.Edit(bookId,"")).ShouldRenderDefaultView().WithModel<Books>(book);
        }

        [TestCase(1)]
        [TestCase(12)]
        [TestCase(14)]
        public void ShouldReturnHtttpStatusCodeNotFoundOnDelete(int bookId)
        {
            var fakeBookRepo = new Mock<IBookFacade>();
            var fakeAuthentication = new Mock<IAuthentication>();
            var fakeAuhtorization = new Mock<IAuthorization>();


            fakeBookRepo.Setup(r => r.GetById(bookId)).Returns((Books)null);

            var sut = new BooksController(fakeAuthentication.Object, fakeAuhtorization.Object, fakeBookRepo.Object);

            var result = sut.WithCallTo(x => x.Delete((int?)null,"")).ShouldGiveHttpStatus(HttpStatusCode.BadRequest);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(10)]
        [TestCase(42)]
        public void ShouldReturnHttpStatusForbiddenOnDelete(int bookId)
        {
            var fakeBookRepo = new Mock<IBookFacade>();
            var fakeAuthentication = new Mock<IAuthentication>();
            var fakeAuhtorization = new Mock<IAuthorization>();

            Books book =
                new Books
                {
                    Title = "Test title 1",
                    Id = bookId,
                    ISBN = "12312432432",
                };

            fakeBookRepo.Setup(r => r.GetById(bookId)).Returns(book);
            fakeAuhtorization.Setup(r => r.IsAuthorizedToDeleteBook(book, It.IsAny<System.Security.Principal.IPrincipal>())).Returns(false);


            var sut = new BooksController(fakeAuthentication.Object, fakeAuhtorization.Object, fakeBookRepo.Object);

            var result = sut.WithCallTo(x => x.Delete(bookId,"")).ShouldGiveHttpStatus(HttpStatusCode.Forbidden);
        }
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(10)]
        [TestCase(42)]
        public void ShouldReturnDefaultDeleteView(int bookId)
        {
            var fakeBookRepo = new Mock<IBookFacade>();
            var fakeAuthentication = new Mock<IAuthentication>();
            var fakeAuhtorization = new Mock<IAuthorization>();

            Books book =
                new Books
                {
                    Title = "Test title 1",
                    Id = bookId,
                    ISBN = "12312432432",
                };


            fakeBookRepo.Setup(r => r.GetById(bookId)).Returns(book);
            fakeAuhtorization.Setup(r => r.IsAuthorizedToDeleteBook(book, It.IsAny<System.Security.Principal.IPrincipal>())).Returns(true);

            var sut = new BooksController(fakeAuthentication.Object, fakeAuhtorization.Object, fakeBookRepo.Object);

            var result = sut.WithCallTo(x => x.Delete(bookId,"")).ShouldRenderDefaultView().WithModel<Books>(book);
        }
    }
}
