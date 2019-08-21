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
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IClientServices _clientServices;
        private readonly ILogger<AuthController> _logger;
        private readonly JwtOptions _jwtOptions;

        public AuthController(IClientServices clientServices, ILogger<AuthController> logger, IOptions<JwtOptions> jwtOptions)
        {
            _clientServices = clientServices;
            _logger = logger;
            _jwtOptions = jwtOptions.Value;
        }

        /// <summary>
        /// Método que faz login no sistemas e obtém o token
        /// </summary>
        /// <param name="login">Propriedades necessárias para login - Cpf e Senha</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<string> Login([FromBody] LoginRequestViewModel login)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(login);

                var result = _clientServices.Login(login);

                return Ok(GenerateJwt(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.ToLogString(Environment.StackTrace));
                return new StatusCodeResult(500);
            }
        }

        private LoginResponseViewModel GenerateJwt(LoginResponseViewModel login)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            var identityClaims = new ClaimsIdentity();
            login.Claims.ForEach(claim => identityClaims.AddClaim(claim));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identityClaims,
                Issuer = _jwtOptions.Issuer,
                //Audience = _jwtOptions.Valid,
                Expires = DateTime.UtcNow.AddHours(_jwtOptions.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            login.Token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            return login;
        }

    }
}