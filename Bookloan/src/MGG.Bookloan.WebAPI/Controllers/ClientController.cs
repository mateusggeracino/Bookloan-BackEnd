﻿using System;
using MGG.Bookloan.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using MGG.Bookloan.Infra;
using MGG.Bookloan.Services.Interfaces;
using MGG.Bookloan.Services.ViewModels.Request;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;

namespace MGG.Bookloan.WebAPI.Controllers
{
    /// <summary>
    /// Controller responsável por prover comportamentos de cliente
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : BaseController
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IClientServices _clientServices;

        /// <summary>
        /// Método construtor da controller cliente.
        /// </summary>
        /// <param name="logger">Gravador de logs</param>
        /// <param name="clientServices">Contrato com comportamentos de cliente</param>
        public ClientController(ILogger<ClientController> logger, IClientServices clientServices)
        {
            _logger = logger;
            _clientServices = clientServices;
        }

        /// <summary>
        /// Obter cliente através do número de cpf
        /// </summary>
        /// <param name="socialNumber">Cpf do Cliente</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<string> Get([FromHeader] string socialNumber)
        {
            try
            {
                _logger.LogInformation("Get a client by social number");
                var client = _clientServices.GetBySocialNumber(socialNumber);
                return Ok(client);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Cadastrar um novo cliente
        /// </summary>
        /// <param name="client">viewmodel do  cliente</param>
        /// <returns></returns>
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

        /// <summary>
        /// Atualizar o cadastro de um cliente
        /// </summary>
        /// <param name="key">Chave do cliente</param>
        /// <param name="client">Entidades com informações de cliente</param>
        /// <returns></returns>
        [HttpPut]
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

        /// <summary>
        /// Inativar um cliente
        /// </summary>
        /// <param name="key">Chave do cliente</param>
        /// <returns></returns>
        [HttpDelete]
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