using InsideTechConf.Library.HealthCheck;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using MGG.Bookloan.Infra;
using MGG.Bookloan.WebAPI.Controllers.Base;

namespace MGG.Bookloan.WebAPI.Controllers
{
    /// <summary>
    /// Controller de HealthCheck
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public sealed class HealthCheckController : BaseController
    {
        private readonly ILogger<HealthCheckController> _logger;

        /// <summary>
        /// Método construtor da controller HealthCheck.
        /// </summary>
        /// <param name="logger">Logger passando a propria controller como parâmetro</param>
        public HealthCheckController(ILogger<HealthCheckController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Método responsável por apresentar as propriedades do HealthCheck do server
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult HealthCheck()
        {
            try
            {
                var healthCheck = HealthCheckHelper.GetGetHostNameAndIPAddress();

                _logger.LogInformation($"{JsonConvert.SerializeObject(healthCheck)}");

                return Ok(healthCheck);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.ToLogString(Environment.StackTrace));
                return new StatusCodeResult(500);
            }
        }
    }
}