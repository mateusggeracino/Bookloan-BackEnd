using System;
using MGG.Bookloan.Infra;
using MGG.Bookloan.Services.Interfaces;
using MGG.Bookloan.Services.ViewModels.Request;
using MGG.Bookloan.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;

namespace MGG.Bookloan.WebAPI.Controllers
{
    /// <summary>
    /// Controller responsável por prover comportamentos de empréstimo
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public sealed class LoanController : BaseController
    {
        private readonly ILoanServices _loanServices;
        private readonly ILogger<LoanController> _logger;

        /// <summary>
        /// Método construtor Empréstimo
        /// </summary>
        /// <param name="loanServices">Contrato com comportamentos de empréstimo</param>
        /// <param name="logger">Contrato com comportamentos de log</param>
        public LoanController(ILoanServices loanServices, ILogger<LoanController> logger)
        {
            _loanServices = loanServices;
            _logger = logger;
        }

        /// <summary>
        /// Obter informações do empréstimo, através do número do cpf.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<string> Get()
        {
            try
            {
                _logger.LogInformation("Get loan information by social number");
                var result = _loanServices.GetByClientKey(ClientKey);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Adiciona um novo empréstimo de livro
        /// </summary>
        /// <param name="loan">Informações do cliente, livro e empréstimo</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<string> Post([FromBody] LoanRequestViewModel loan)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(loan);
                var result = _loanServices.Add(loan);

                if (result.ValidationResult.Errors.Any()) return AddValidationErrors(result.ValidationResult.Errors);

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