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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Moq.Language.Flow;
using Xunit;

namespace MGG.Bookloan.Tests.Controller
{
    public class ClientControllerTests
    {
        /// <summary>
        /// POST - Simulação do comportamento de erros e sucesso com os status code (200,400 e 500)
        /// </summary>
        #region Post
        [Fact(DisplayName = "Post - Success - StatusCode(200)")]
        public void PostSuccess()
        {
            var clientServices = new Mock<IClientServices>();
            var logger = new Mock<ILogger<ClientController>>();
            var clientRequest = A.New<ClientRequestViewModel>();
            A.Configure<ClientResponseViewModel>()
                .Fill(x => x.ValidationResult, new ValidationResult());

            var clientResponse = A.New<ClientResponseViewModel>();
            var clientController = new ClientController(logger.Object, clientServices.Object);

            clientServices.Setup(x => x.Add(It.IsAny<ClientRequestViewModel>())).Returns(clientResponse);
            var response = clientController.Post(clientRequest);

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
            var clientServices = new Mock<IClientServices>();
            var logger = new Mock<ILogger<ClientController>>();
            var clientRequest = A.New<ClientRequestViewModel>();
            var validationResult = new ValidationResult
            {
                Errors = { new ValidationFailure("SocialNumber", "Invalid") }
            };

            A.Configure<ClientResponseViewModel>()
                .Fill(x => x.ValidationResult, validationResult);

            var clientResponse = A.New<ClientResponseViewModel>();
            var clientController = new ClientController(logger.Object, clientServices.Object);

            clientServices.Setup(x => x.Add(It.IsAny<ClientRequestViewModel>())).Returns(clientResponse);
            var response = clientController.Post(clientRequest);

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
            var clientServices = new Mock<IClientServices>();
            var logger = new Mock<ILogger<ClientController>>();
            var clientRequest = A.New<ClientRequestViewModel>();
            A.Configure<ClientResponseViewModel>()
                .Fill(x => x.ValidationResult, x => null);
            var clientResponse = A.New<ClientResponseViewModel>();
            var clientController = new ClientController(logger.Object, clientServices.Object);

            clientServices.Setup(x => x.Add(It.IsAny<ClientRequestViewModel>())).Returns(clientResponse);
            var response = clientController.Post(clientRequest);

            Assert.NotNull(response);
            Assert.IsType<StatusCodeResult>(response.Result);

            var httpObjResult = response.Result as StatusCodeResult;

            Assert.NotNull(httpObjResult);
            Assert.True(httpObjResult.StatusCode == 500);
        }
        #endregion

        /// <summary>
        /// PUT - Simulação do comportamento de erros e sucesso com os status code (200,400 e 500)
        /// </summary>
        #region Put
        [Fact(DisplayName = "Put - Success - StatusCode(200)")]
        public void PutSuccess()
        {
            var clientServices = new Mock<IClientServices>();
            var logger = new Mock<ILogger<ClientController>>();
            var clientRequest = A.New<ClientRequestViewModel>();
            A.Configure<ClientResponseViewModel>()
                .Fill(x => x.ValidationResult, new ValidationResult());
            var clientResponse = A.New<ClientResponseViewModel>();
            var clientController = new ClientController(logger.Object, clientServices.Object);
            clientServices.Setup(x => x.Update(It.IsAny<Guid>(), It.IsAny<ClientRequestViewModel>()))
                .Returns(clientResponse);

            var response = clientController.Put(clientRequest);

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
            var clientServices = new Mock<IClientServices>();
            var logger = new Mock<ILogger<ClientController>>();
            var clientRequest = A.New<ClientRequestViewModel>();
            var validationResult = new ValidationResult
            {
                Errors = { new ValidationFailure("Error", "Test Error") }
            };

            A.Configure<ClientResponseViewModel>()
                .Fill(x => x.ValidationResult, validationResult);

            var clientResponse = A.New<ClientResponseViewModel>();
            var clientController = new ClientController(logger.Object, clientServices.Object);
            clientServices.Setup(x => x.Update(It.IsAny<Guid>(), It.IsAny<ClientRequestViewModel>()))
                .Returns(clientResponse);

            var response = clientController.Put(clientRequest);

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
            var clientServices = new Mock<IClientServices>();
            var logger = new Mock<ILogger<ClientController>>();
            var clientRequest = A.New<ClientRequestViewModel>();
            A.Configure<ClientResponseViewModel>()
                .Fill(x => x.ValidationResult, x => null);
            var clientResponse = A.New<ClientResponseViewModel>();
            var clientController = new ClientController(logger.Object, clientServices.Object);
            clientServices.Setup(x => x.Update(It.IsAny<Guid>(), It.IsAny<ClientRequestViewModel>()))
                .Returns(clientResponse);

            var response = clientController.Put(clientRequest);

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
            var clientServices = new Mock<IClientServices>();
            var logger = new Mock<ILogger<ClientController>>();
            var clientResponse = A.New<ClientResponseViewModel>();
            var clientController = new ClientController(logger.Object, clientServices.Object);
            clientServices.Setup(x => x.GetByKey(It.IsAny<Guid>()))
                .Returns(clientResponse);

            var response = clientController.Get();

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
            var clientServices = new Mock<IClientServices>();
            var logger = new Mock<ILogger<ClientController>>();
            var clientController = new ClientController(logger.Object, clientServices.Object);
            clientServices.Setup(x => x.GetByKey(It.IsAny<Guid>())).Throws(new IOException());
            
            var response = clientController.Get();

            Assert.NotNull(response);
            Assert.IsType<StatusCodeResult>(response.Result);

            var httpObjResult = response.Result as StatusCodeResult;

            Assert.NotNull(httpObjResult);
            Assert.True(httpObjResult.StatusCode == 500);
        }
        #endregion

        /// <summary>
        /// Delete - Simulação do comportamento de erro e sucesso com os status code (200 e 500)
        /// </summary>
        #region Delete
        [Fact(DisplayName = "Delete - Success - StatusCode(200)")]
        public void DeleteSuccess()
        {
            var clientServices = new Mock<IClientServices>();
            var logger = new Mock<ILogger<ClientController>>();
            var clientController = new ClientController(logger.Object, clientServices.Object);
            clientServices.Setup(x => x.Inactivate(It.IsAny<Guid>())).Returns(true);

            var response = clientController.Delete();

            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response.Result);

            var httpObjResult = response.Result as OkObjectResult;

            Assert.NotNull(httpObjResult);
            Assert.True(httpObjResult.StatusCode == 200);

            var value = httpObjResult.Value;
            Assert.NotNull(value);
            Assert.False(string.IsNullOrEmpty(value.ToString()));
        }

        [Fact(DisplayName = "Delete - Fail - StatusCode(500)")]
        public void DeleteInternFail()
        {
            var clientServices = new Mock<IClientServices>();
            var logger = new Mock<ILogger<ClientController>>();
            var clientController = new ClientController(logger.Object, clientServices.Object);
            clientServices.Setup(x => x.Inactivate(It.IsAny<Guid>())).Throws(new IOException());

            var response = clientController.Delete();

            Assert.NotNull(response);
            Assert.IsType<StatusCodeResult>(response.Result);

            var httpObjResult = response.Result as StatusCodeResult;

            Assert.NotNull(httpObjResult);
            Assert.True(httpObjResult.StatusCode == 500);
        }
        #endregion
    }
}