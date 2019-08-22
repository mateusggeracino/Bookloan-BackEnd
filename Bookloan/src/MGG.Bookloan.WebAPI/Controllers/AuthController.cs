using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MGG.Bookloan.Infra;
using MGG.Bookloan.Services.Interfaces;
using MGG.Bookloan.Services.ViewModels.Jwt;
using MGG.Bookloan.Services.ViewModels.Request;
using MGG.Bookloan.Services.ViewModels.Response;
using MGG.Bookloan.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MGG.Bookloan.WebAPI.Controllers
{
    /// <summary>
    /// Controller responsável por garantir a autenticação.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public sealed class AuthController : BaseController
    {
        private readonly IClientServices _clientServices;
        private readonly ILogger<AuthController> _logger;
        private readonly JwtOptions _jwtOptions;

        /// <summary>
        /// Método construtor da classe de Authorize
        /// </summary>
        /// <param name="clientServices">Comportamentos de cliente</param>
        /// <param name="logger">Comportamentos de logger</param>
        /// <param name="jwtOptions">IOptions obtém informações do appsettings através da DI e converte para a propriedade de JwtOptions</param>
        public AuthController(IClientServices clientServices, ILogger<AuthController> logger, IOptions<JwtOptions> jwtOptions)
        {
            _clientServices = clientServices;
            _logger = logger;
            _jwtOptions = jwtOptions.Value;
        }

        /// <summary>
        /// Efetua login no sistema
        /// </summary>
        /// <param name="login">Propriedades necessárias para login - Cpf e Senha</param>
        /// <returns>Retorna usuário com token de acesso</returns>
        [HttpPost]
        public ActionResult<string> Login([FromBody] LoginRequestViewModel login)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(login);

                var result = _clientServices.Login(login);

                if (result == null) return BadRequest(Labels.UserNotFound);

                return Ok(GenerateJwt(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.ToLogString(Environment.StackTrace));
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Gerador de token com base nas informações do usuário
        /// </summary>
        /// <param name="login">Propriedades de login response</param>
        /// <returns></returns>
        private LoginResponseViewModel GenerateJwt(LoginResponseViewModel login)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(login.Claims);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.ValidIn,
                Expires = DateTime.UtcNow.AddHours(_jwtOptions.ExpirationInHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };

            login.Token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            return login;
        }

    }
}