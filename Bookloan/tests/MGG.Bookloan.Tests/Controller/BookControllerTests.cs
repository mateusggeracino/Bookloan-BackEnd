using System;
using FluentValidation.Results;
using GenFu;
using MGG.Bookloan.Infra;
using MGG.Bookloan.Services.Interfaces;
using MGG.Bookloan.Services.ViewModels.Request;
using MGG.Bookloan.Services.ViewModels.Response;
using MGG.Bookloan.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace MGG.Bookloan.Tests.Controller
{
    public class BookControllerTests
    {
        /// <summary>
        /// Get - Simulação do comportamento de erro e sucesso com os status code (200 e 500)
        /// </summary>
        #region Get
        [Fact(DisplayName = "Get - Success - StatusCode(200)")]
        public void GetSuccess()
        {
            var bookServices = new Mock<IBookServices>();
            var logger = new Mock<ILogger<BookController>>();
            var bookResponse = A.ListOf<BookResponseViewModel>(10);
            var bookController = new BookController(bookServices.Object, logger.Object);

            bookServices.Setup(x => x.GetAll()).Returns(bookResponse);
            var response = bookController.Get();

            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response.Result);

            var httpObjResult = response.Result as OkObjectResult;

            Assert.NotNull(httpObjResult);
            Assert.True(httpObjResult.StatusCode == 200);

            var value = httpObjResult.Value as List<BookResponseViewModel>;
            Assert.NotNull(value);
            Assert.False(string.IsNullOrEmpty(value.ToString()));
            Assert.True(value.Count > 0);
        }

        [Fact(DisplayName = "Get - Fail - StatusCode(500)")]
        public void GetFail()
        {
            var bookServices = new Mock<IBookServices>();
            var logger = new Mock<ILogger<BookController>>();
            var bookController = new BookController(bookServices.Object, logger.Object);

            bookServices.Setup(x => x.GetAll()).Throws(new IOException());
            var response = bookController.Get();

            Assert.NotNull(response);
            Assert.IsType<StatusCodeResult>(response.Result);

            var httpObjResult = response.Result as StatusCodeResult;

            Assert.NotNull(httpObjResult);
            Assert.True(httpObjResult.StatusCode == 500);
        }
        #endregion

        #region  Post
        [Fact(DisplayName = "Post - Success - StatusCode(200)")]
        public void PostSuccess()
        {
            var bookServices = new Mock<IBookServices>();
            var logger = new Mock<ILogger<BookController>>();
            var bookRequest = A.New<BookRequestViewModel>();
            A.Configure<BookResponseViewModel>()
                .Fill(x => x.ValidationResult, A.New<ValidationResult>());
            var bookResponse = A.New<BookResponseViewModel>();

            var bookController = new BookController(bookServices.Object, logger.Object);
            bookServices.Setup(x => x.Add(It.IsAny<BookRequestViewModel>())).Returns(bookResponse);

            var response = bookController.Post(bookRequest);

            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response.Result);

            var httpObjResult = response.Result as OkObjectResult;

            Assert.NotNull(httpObjResult);
            Assert.True(httpObjResult.StatusCode == 200);

            var value = httpObjResult.Value;
            Assert.NotNull(value);
            Assert.False(string.IsNullOrEmpty(value.ToString()));
            Assert.Same(Labels.Success, value);
        }

        [Fact(DisplayName = "Post - Fail - StatusCode(400)")]
        public void PostFail()
        {
            var bookServices = new Mock<IBookServices>();
            var logger = new Mock<ILogger<BookController>>();
            var bookRequest = A.New<BookRequestViewModel>();
            var validationResult = new ValidationResult
            {
                Errors = { new ValidationFailure("Title", "Invalid") }
            };

            A.Configure<BookResponseViewModel>()
                .Fill(x => x.ValidationResult, validationResult);

            var bookResponse = A.New<BookResponseViewModel>();
            var bookController = new BookController(bookServices.Object, logger.Object);

            bookServices.Setup(x => x.Add(It.IsAny<BookRequestViewModel>())).Returns(bookResponse);
            var response = bookController.Post(bookRequest);

            Assert.NotNull(response);
            Assert.IsType<BadRequestObjectResult>(response.Result);

            var httpObjResult = response.Result as BadRequestObjectResult;

            Assert.NotNull(httpObjResult);
            Assert.True(httpObjResult.StatusCode == 400);

            var value = httpObjResult.Value;
            Assert.NotNull(value);
            Assert.False(string.IsNullOrEmpty(value.ToString()));
        }

        [Fact(DisplayName = "Post - Fail - StatusCode(500)")]
        public void PostInternFail()
        {
            var bookServices = new Mock<IBookServices>();
            var logger = new Mock<ILogger<BookController>>();
            var bookRequest = A.New<BookRequestViewModel>();
            var bookController = new BookController(bookServices.Object, logger.Object);

            bookServices.Setup(x => x.Add(It.IsAny<BookRequestViewModel>())).Throws(new IOException());

            var response = bookController.Post(bookRequest);

            Assert.NotNull(response);
            Assert.IsType<StatusCodeResult>(response.Result);

            var httpObjResult = response.Result as StatusCodeResult;

            Assert.NotNull(httpObjResult);
            Assert.True(httpObjResult.StatusCode == 500);
        }
        #endregion

        #region Put
        #region Put
        [Fact(DisplayName = "Put - Success - StatusCode(200)")]
        public void PutSuccess()
        {
            var bookServices = new Mock<IBookServices>();
            var logger = new Mock<ILogger<BookController>>();
            var bookRequest = A.New<BookRequestViewModel>();
            A.Configure<BookResponseViewModel>()
                .Fill(x => x.ValidationResult, A.New<ValidationResult>());
            var bookResponse = A.New<BookResponseViewModel>();
            var bookController = new BookController(bookServices.Object, logger.Object);
            bookServices.Setup(x => x.Update(It.IsAny<Guid>(), It.IsAny<BookRequestViewModel>()))
                .Returns(bookResponse);

            var response = bookController.Put(Guid.NewGuid(), bookRequest);

            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response.Result);

            var httpObjResult = response.Result as OkObjectResult;

            Assert.NotNull(httpObjResult);
            Assert.True(httpObjResult.StatusCode == 200);

            var value = httpObjResult.Value;
            Assert.NotNull(value);
            Assert.False(string.IsNullOrEmpty(value.ToString()));
            Assert.Same(Labels.Success, value);
        }

        [Fact(DisplayName = "Put - Fail - StatusCode(400)")]
        public void PutFail()
        {
            var bookServices = new Mock<IBookServices>();
            var logger = new Mock<ILogger<BookController>>();
            var bookRequest = A.New<BookRequestViewModel>();
            var validationResult = new ValidationResult
            {
                Errors = { new ValidationFailure("Error", "Test Error") }
            };

            A.Configure<BookResponseViewModel>()
                .Fill(x => x.ValidationResult, validationResult);

            var bookResponse = A.New<BookResponseViewModel>();
            var bookController = new BookController(bookServices.Object, logger.Object);
            bookServices.Setup(x => x.Update(It.IsAny<Guid>(), It.IsAny<BookRequestViewModel>()))
                .Returns(bookResponse);

            var response = bookController.Put(Guid.NewGuid(), bookRequest);

            Assert.NotNull(response);
            Assert.IsType<BadRequestObjectResult>(response.Result);

            var httpObjResult = response.Result as BadRequestObjectResult;

            Assert.NotNull(httpObjResult);
            Assert.True(httpObjResult.StatusCode == 400);

            var value = httpObjResult.Value as List<string>;
            Assert.NotNull(value);
            Assert.True(value.Any());
            Assert.False(string.IsNullOrEmpty(value.First()));
            Assert.Same(value.First(), "Test Error");
        }

        [Fact(DisplayName = "Put - Fail - StatusCode(500)")]
        public void PutInternFail()
        {
            var bookServices = new Mock<IBookServices>();
            var logger = new Mock<ILogger<BookController>>();
            var bookRequest = A.New<BookRequestViewModel>();
            A.Configure<BookResponseViewModel>()
                .Fill(x => x.ValidationResult, x => null);
            var bookResponse = A.New<BookResponseViewModel>();
            var bookController = new BookController(bookServices.Object, logger.Object);
            bookServices.Setup(x => x.Update(It.IsAny<Guid>(), It.IsAny<BookRequestViewModel>()))
                .Returns(bookResponse);

            var response = bookController.Put(Guid.NewGuid(), bookRequest);

            Assert.NotNull(response);
            Assert.IsType<StatusCodeResult>(response.Result);

            var httpObjResult = response.Result as StatusCodeResult;

            Assert.NotNull(httpObjResult);
            Assert.True(httpObjResult.StatusCode == 500);
        }
        #endregion
        #endregion
    }
}