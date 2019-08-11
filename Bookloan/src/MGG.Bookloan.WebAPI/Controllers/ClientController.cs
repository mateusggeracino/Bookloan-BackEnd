using System;
using System.Runtime.InteropServices.WindowsRuntime;
using MGG.Bookloan.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using MGG.Bookloan.Infra;
using MGG.Bookloan.Services.Interfaces;
using MGG.Bookloan.Services.ViewModels.Request;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;

namespace MGG.Bookloan.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : BaseController
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IClientServices _clientServices;

        public ClientController(ILogger<ClientController> logger, IClientServices clientServices)
        {
            _logger = logger;
            _clientServices = clientServices;
        }

        [HttpGet("{key}")]
        public ActionResult<string> Get([FromHeader] Guid key)
        {
            try
            {
                _logger.LogInformation("Get a client by key");
                var client = _clientServices.GetByKey(key);
                return Ok(client);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new StatusCodeResult(500);
            }
        }

        [HttpPost]
        public ActionResult<string> Post([FromBody] ClientRequestViewModel client)
        {
            try
            {
                _logger.LogInformation("Add a new client");
                if (!ModelState.IsValid) return BadRequest(client);

                var result = _clientServices.Add(client);
                if (result.ValidationResult.Errors.Any()) return AddValidationErrors(result.ValidationResult.Errors);

                return Ok(Labels.Success);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new StatusCodeResult(500);
            }
        }

        [HttpPut("{key}")]
        public ActionResult<string> Put([FromHeader]Guid key, [FromBody] ClientRequestViewModel client)
        {
            try
            {
                _logger.LogInformation("Update a Client");
                var result = _clientServices.Update(key, client);
                if (result.ValidationResult.Errors.Any()) return AddValidationErrors(result.ValidationResult.Errors);

                return Ok(Labels.Success);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new StatusCodeResult(500);
            }
        }

        [HttpDelete("{key}")]
        public ActionResult<string> Delete([FromHeader] Guid key)
        {
            try
            {
                _logger.LogInformation("Inactivate a client");
                _clientServices.Inactivate(key);

                return Ok(Labels.Success);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new StatusCodeResult(500);
            }
        }
    }
}