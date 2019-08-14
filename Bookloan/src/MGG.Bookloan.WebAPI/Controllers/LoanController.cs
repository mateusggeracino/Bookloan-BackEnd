using System;
using MGG.Bookloan.Services.Interfaces;
using MGG.Bookloan.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MGG.Bookloan.WebAPI.Controllers
{
    /// <summary>
    /// Controller responsável por prover comportamentos de empréstimo
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : BaseController
    {
        private readonly ILoanServices _loanServices;
        private readonly ILogger<LoanController> _logger;

        public LoanController(ILoanServices loanServices, ILogger<LoanController> logger)
        {
            _loanServices = loanServices;
            _logger = logger;
        }

        /// <summary>
        /// Obter informações do empréstimo, através do número do cpf.
        /// </summary>
        /// <param name="socialNumber"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<string> Get([FromHeader] string socialNumber)
        {
            try
            {
                _logger.LogInformation("Get loan information by social number");
                var result = _loanServices.GetBySocialNumber(socialNumber);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new StatusCodeResult(500);
            }
        }
    }
}