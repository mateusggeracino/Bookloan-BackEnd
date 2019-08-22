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
    public class ClientControllerTests
    {
        [Fact(DisplayName = "Create a new user - Success")]
        public void PostUserSuccess()
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

        [Fact(DisplayName = "Create a new user - Fail")]
        public void PostUserFail()
        {
            var clientServices = new Mock<IClientServices>();
            var logger = new Mock<ILogger<ClientController>>();
            var clientRequest = A.New<ClientRequestViewModel>();
            var validationResult= new ValidationResult
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
    }
}