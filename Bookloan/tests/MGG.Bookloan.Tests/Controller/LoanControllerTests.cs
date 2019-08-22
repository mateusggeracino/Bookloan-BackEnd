using System;
using System.IO;
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
using Xunit;

namespace MGG.Bookloan.Tests.Controller
{
    public class LoanControllerTests
    {
        /// <summary>
        /// POST - Simulação do comportamento de erros e sucesso com os status code (200,400 e 500)
        /// </summary>
        #region Post
        [Fact(DisplayName = "Post - Success - StatusCode(200)")]
        public void PostSuccess()
        {
            var loanServices = new Mock<ILoanServices>();
            var logger = new Mock<ILogger<LoanController>>();
            var loanRequest = A.New<LoanRequestViewModel>();
            A.Configure<LoanResponseViewModel>()
                .Fill(x => x.ValidationResult, new ValidationResult());

            var loanResponse = A.New<LoanResponseViewModel>();
            var loanController = new LoanController(loanServices.Object, logger.Object);

            loanServices.Setup(x => x.Add(It.IsAny<LoanRequestViewModel>())).Returns(loanResponse);
            var response = loanController.Post(loanRequest);

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
            var loanServices = new Mock<ILoanServices>();
            var logger = new Mock<ILogger<LoanController>>();
            var loanRequest = A.New<LoanRequestViewModel>();
            var validationResult = new ValidationResult
            {
                Errors = { new ValidationFailure("SocialNumber", "Invalid") }
            };

            A.Configure<LoanResponseViewModel>()
                .Fill(x => x.ValidationResult, validationResult);

            var loanResponse = A.New<LoanResponseViewModel>();
            var loanController = new LoanController(loanServices.Object, logger.Object);

            loanServices.Setup(x => x.Add(It.IsAny<LoanRequestViewModel>())).Returns(loanResponse);
            var response = loanController.Post(loanRequest);

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
            var loanServices = new Mock<ILoanServices>();
            var logger = new Mock<ILogger<LoanController>>();
            var loanRequest = A.New<LoanRequestViewModel>();
            A.Configure<LoanResponseViewModel>()
                .Fill(x => x.ValidationResult, x => null);
            var loanResponse = A.New<LoanResponseViewModel>();
            var loanController = new LoanController(loanServices.Object,logger.Object);

            loanServices.Setup(x => x.Add(It.IsAny<LoanRequestViewModel>())).Returns(loanResponse);
            var response = loanController.Post(loanRequest);

            Assert.NotNull(response);
            Assert.IsType<StatusCodeResult>(response.Result);

            var httpObjResult = response.Result as StatusCodeResult;

            Assert.NotNull(httpObjResult);
            Assert.True(httpObjResult.StatusCode == 500);
        }
        #endregion

        /// <summary>
        /// GET - Simulação do comportamento de erro e sucesso com os status code (200 e 500)
        /// </summary>
        #region Get
        [Fact(DisplayName = "Get - Success - StatusCode(200)")]
        public void GetSuccess()
        {
            var loanServices = new Mock<ILoanServices>();
            var logger = new Mock<ILogger<LoanController>>();
            var loanResponse = A.ListOf<LoanResponseViewModel>();
            var loanController = new LoanController(loanServices.Object, logger.Object);
            loanServices.Setup(x => x.GetByClientKey(It.IsAny<Guid>()))
                .Returns(loanResponse);

            var response = loanController.Get();

            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response.Result);

            var httpObjResult = response.Result as OkObjectResult;

            Assert.NotNull(httpObjResult);
            Assert.True(httpObjResult.StatusCode == 200);

            var value = httpObjResult.Value;
            Assert.NotNull(value);
            Assert.False(string.IsNullOrEmpty(value.ToString()));
        }

        [Fact(DisplayName = "Get - Fail - StatusCode(500)")]
        public void GetInternFail()
        {
            var loanServices = new Mock<ILoanServices>();
            var logger = new Mock<ILogger<LoanController>>();
            var loanController = new LoanController(loanServices.Object, logger.Object);
            loanServices.Setup(x => x.GetByClientKey(It.IsAny<Guid>())).Throws(new IOException());

            var response = loanController.Get();

            Assert.NotNull(response);
            Assert.IsType<StatusCodeResult>(response.Result);

            var httpObjResult = response.Result as StatusCodeResult;

            Assert.NotNull(httpObjResult);
            Assert.True(httpObjResult.StatusCode == 500);
        }
        #endregion
    }
}